using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using UmbracoRaffle.Data;
using UmbracoRaffle.Models;

namespace UmbracoRaffle.Controllers
{
    public class RaffleController : Controller
    {
        private readonly RaffleDbContext _context;

        public RaffleController(RaffleDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public ViewResult Index(string searchString, int page = 0)
        {
            if (searchString != null)
            {
                page = 1;
            }

            ViewBag.CurrentFilter = searchString;
            var entries = from e in _context.Raffle
                          select e;
            
            
            if (!String.IsNullOrEmpty(searchString))
            {
                entries = entries.Where(e => e.Lastname.ToLower().Contains(searchString.ToLower())
                                   || e.Firstname.ToLower().Contains(searchString.ToLower()));
            }


            int NoOfEntries = entries.Count();
            int pageSize = 10;
            var data = entries.Skip(page * pageSize).Take(pageSize).ToList();
            ViewBag.MaxPage = (NoOfEntries / pageSize) - (NoOfEntries % pageSize == 0 ? 1 : 0);
            ViewBag.Page = page;
            return View(data);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raffle = await _context.Raffle
                .FirstOrDefaultAsync(m => m.ID == id);
            if (raffle == null)
            {
                return NotFound();
            }

            return View(raffle);
        }

        // GET: Raffle/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Firstname,Lastname,Email,Age,Serialnumber")] Raffle raffle)
        {
            if (ModelState.IsValid)
            {
                // _context.Serialnumbers.Where(s => s.Number.Equals(raffle.Serialnumber)).Any();
                if (_context.Serialnumbers.Any(s => s.Number == raffle.Serialnumber))
                {
                    if (_context.Raffle.Where(r => r.Serialnumber.Equals(raffle.Serialnumber)).Count() < 2)
                    {
                        _context.Add(raffle);
                        await _context.SaveChangesAsync();
                        return View("Entered");
                    }
                }
            }
            ViewBag.Error = "The serial is either invalid, or it has been entered in the raffle 2 times";
            return View();
        }
        [Authorize]
        // GET: Raffle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raffle = await _context.Raffle.FindAsync(id);
            if (raffle == null)
            {
                return NotFound();
            }
            return View(raffle);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Firstname,Lastname,Email,Number")] Raffle raffle)
        {
            if (id != raffle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raffle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaffleExists(raffle.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(raffle);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raffle = await _context.Raffle
                .FirstOrDefaultAsync(m => m.ID == id);
            if (raffle == null)
            {
                return NotFound();
            }

            return View(raffle);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raffle = await _context.Raffle.FindAsync(id);
            _context.Raffle.Remove(raffle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaffleExists(int id)
        {
            return _context.Raffle.Any(e => e.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        // GET: Raffle
        public async Task<IActionResult> Index()
        {
            return View(await _context.Raffle.ToListAsync());
        }

        // GET: Raffle/Details/5
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

        [HttpGet]
        public async Task<IActionResult> ValidateSerialAsync(int serial)
        {
            if (await _context.Serialnumbers.AnyAsync(s => s.Number == serial))
            {
                if (_context.Raffle.Where(r => r.Serialnumber.Equals(serial)).Count() < 2)
                {
                    Create();
                }
            }
            ViewBag.Error = "The serial is either invalid, or it has been entered in the raffle 2 times";
            return Ok();
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
                        return RedirectToAction(nameof(Index));
                    }
                }
                ViewBag.Error = "The serial is either invalid, or it has been entered in the raffle 2 times";

            }
            return View("Congrats");
        }

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

        // POST: Raffle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Raffle/Delete/5
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

        // POST: Raffle/Delete/5
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

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmbracoRaffle.Models;

namespace UmbracoRaffle.Data
{
    public class RaffleDbContext : DbContext
    {

            public RaffleDbContext(DbContextOptions<RaffleDbContext> options)
                : base(options)
            {
            }

            public DbSet<UmbracoRaffle.Models.Raffle> Raffle { get; set; }
            public DbSet<Serialnumber> Serialnumbers { get; set; }
    }
}

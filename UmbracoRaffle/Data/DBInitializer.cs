using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmbracoRaffle.Models;

namespace UmbracoRaffle.Data
{
    public class DBInitializer
    {
        public static void Initialize(RaffleDbContext context)
        {
            System.IO.File.WriteAllLines("SerialNumbers.txt",
            context.Serialnumbers.Select(s => s.Number.ToString()));        //Write Serialnumbers to txt file

            context.Database.EnsureCreated();

            if (context.Serialnumbers.Any())
            {
                return; //database has been created
            }

            Random rnd = new Random();  // Generate Random Number

            var serialnumber = new Serialnumber[100];

            for (int i = 0; i < 100; i++)
            {
            serialnumber[i] = new Serialnumber { Number = rnd.Next(1, 34000) };
            };

            foreach (Serialnumber s in serialnumber)
            {
                context.Serialnumbers.Add(s);       //Add numbers to DB
            }



            context.SaveChanges();
            
        }
    }
}

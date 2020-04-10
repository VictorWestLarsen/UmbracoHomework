using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UmbracoRaffle.Controllers;
using UmbracoRaffle.Data;
using UmbracoRaffle.Models;
using Xunit;

namespace AcmeUnitTest
{
    public class RaffleControllerTest
    {
        DbContextOptions<RaffleDbContext> options;
        [Fact]
        public async void TestInvalidAge()
        {
            options = new DbContextOptionsBuilder<RaffleDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCreatInvalidAgeEntryDatabase").Options;
            RaffleDbContext context = new RaffleDbContext(options);
            RaffleController controller = new RaffleController(context);
            context.Serialnumbers.Add(new Serialnumber { Number = 6821 });
            context.SaveChanges();

            Raffle rafInvalid = new Raffle
            {
                Firstname = "John",
                Lastname = "Doe",
                Age = 17,
                Number = 6821,
                Email = "jonh@doe.com"
            };

            var result = await controller.Create(rafInvalid);

            Assert.False(context.Raffle.Any(), "Entry was added, with invalid age");
        }

        [Fact]
        public async void TestValidAge()
        {
            options = new DbContextOptionsBuilder<RaffleDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCreateValidEntryDatabase").Options;
            RaffleDbContext context = new RaffleDbContext(options);
            RaffleController controller = new RaffleController(context);
            context.Serialnumbers.Add(new Serialnumber { Number = 6821 });

            Raffle rafValid = new Raffle
            {
                Firstname = "Jane",
                Lastname = "Doe",
                Age = 20,
                Number = 6821,
                Email = "jane@doe.com"
            };

            context.SaveChanges();
            var result = await controller.Create(rafValid);

            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            Assert.True(context.Raffle.Any(), "Entry wasn't added, with valid age and serial");

        }
        [Fact]
        public async void TestInvalidSerial()
        {
            options = new DbContextOptionsBuilder<RaffleDbContext>()
            .UseInMemoryDatabase(databaseName: "TestInvalidEntryDatabase").Options;
            RaffleDbContext context = new RaffleDbContext(options);
            RaffleController controller = new RaffleController(context);
            context.Serialnumbers.Add(new Serialnumber { Number = 1754 });

            Raffle rafValid = new Raffle
            {
                Firstname = "Jane",
                Lastname = "Doe",
                Age = 20,
                Number = 6821,
                Email = "jane@doe.com"
            };

            context.SaveChanges();
            var result = await controller.Create(rafValid);

            Assert.False(context.Raffle.Any(), "Entry was added, with invalid Serial");

        }
        [Fact]
        public async void TestMoreThanTwoEntriesSerial()
        {
            options = new DbContextOptionsBuilder<RaffleDbContext>()
            .UseInMemoryDatabase(databaseName: "TestSerialEnteredTwoTimesDatabase").Options;
            RaffleDbContext context = new RaffleDbContext(options);
            RaffleController controller = new RaffleController(context);
            context.Serialnumbers.Add(new Serialnumber { Number = 1754 });
            context.Raffle.Add(new Raffle
            {
                Firstname = "Jane",
                Lastname = "Doe",
                Age = 20,
                Number = 1754,
                Email = "jane@doe.com"
            });
            context.Raffle.Add(new Raffle
            {
                Firstname = "John",
                Lastname = "Doe",
                Age = 20,
                Number = 1754,
                Email = "John@doe.com"
            });

            Raffle rafSerial = new Raffle
            {
                Firstname = "Bobby",
                Lastname = "Doe",
                Age = 20,
                Number = 1754,
                Email = "booby@doe.com"
            };

            context.SaveChanges();
            var result = await controller.Create(rafSerial);


            Assert.True(context.Raffle.Where(r => r.Number.Equals(rafSerial.Number)).Count() == 2, "Entry was added, with serial that has been entered in the raffle two times");

        }
    }
}

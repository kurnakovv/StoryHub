﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryHub.BL.Models;
using StoryHub.BL.Services;
using StoryHub.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryHub.Tests.BL.Services
{
    [TestClass]
    public class StorytellerServiceTest
    {
        [TestMethod]
        public void CanGetAllStorytellers()
        {
            IEnumerable<Storyteller> storytellers = new List<Storyteller>
            {
                new Storyteller
                {
                    Id = "5050241f-a600-4d64-8634-0de904c043c1",
                    AccessFailedCount = 0,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    UserName = "UserName",
                    NormalizedUserName = "USERNAME",
                    Email = "simple@gmail.com",
                    NormalizedEmail = "SIMPLE@gmail.com",
                    PasswordHash = "Pasfjdsasd",
                    ConcurrencyStamp = "fjjffjfjf",
                    LockoutEnd = DateTime.Now,
                    PhoneNumber = "885553531",
                    SecurityStamp = "Securitystamp",
                },
                new Storyteller
                {
                    Id = "5050241f-a600-4d64-8634-0de904c043c1",
                    AccessFailedCount = 0,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    UserName = "UserName",
                    NormalizedUserName = "USERNAME",
                    Email = "simple@gmail.com",
                    NormalizedEmail = "SIMPLE@gmail.com",
                    PasswordHash = "Pasfjdsasd",
                    ConcurrencyStamp = "fjjffjfjf",
                    LockoutEnd = DateTime.Now,
                    PhoneNumber = "885553531",
                    SecurityStamp = "Securitystamp",
                },
            };

            IStorytellerService storytellerService = new StorytellerService();

            var result = storytellerService.GetAllStorytellers();

            Assert.AreEqual(storytellers, result);
        }

        [TestMethod]
        public void CanCreateValidStoryteller_ReturnId()
        {

            Storyteller storyteller = new Storyteller("Name", true, "img.png", "About", 18)
            {
                Id = "5050241f-a600-4d64-8634-0de904c043c1",
                AccessFailedCount = 0,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                UserName = "UserName",
                NormalizedUserName = "USERNAME",
                Email = "simple@gmail.com",
                NormalizedEmail = "SIMPLE@gmail.com",
                PasswordHash = "Pasfjdsasd",
                ConcurrencyStamp = "fjjffjfjf",
                LockoutEnd = DateTime.Now,
                PhoneNumber = "885553531",
                SecurityStamp = "Securitystamp",
            };

            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerCRUD storytellerService2 = new StorytellerService();

            Task<string> result = storytellerService1.CreateStoryteller(storyteller);
            storytellerService2.DeleteStorytellerById(storyteller.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(storyteller);
            Assert.AreEqual(storyteller.Id, result.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CannotCreateStorytellerIfAlreadyExists_ReturnException()
        {
            Storyteller storyteller = new Storyteller("Name", true, "img.png", "About", 18)
            {
                Id = "5050241f-a600-4d64-8634-0de904c043c1",
                AccessFailedCount = 0,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                UserName = "UserName",
                NormalizedUserName = "USERNAME",
                Email = "simple@gmail.com",
                NormalizedEmail = "SIMPLE@gmail.com",
                PasswordHash = "Pasfjdsasd",
                ConcurrencyStamp = "fjjffjfjf",
                LockoutEnd = DateTime.Now,
                PhoneNumber = "885553531",
                SecurityStamp = "Securitystamp",
            };

            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerCRUD storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            await storytellerService1.CreateStoryteller(storyteller);
            await storytellerService2.CreateStoryteller(storyteller);
            await storytellerService3.DeleteStorytellerById(storyteller.Id);
        }

        [TestMethod]
        public void CanDeleteStorytellerByValidId()
        {
            Storyteller storyteller = new Storyteller("Name", true, "img.png", "About", 18)
            {
                Id = "5050241f-a600-4d64-8634-0de904c043c1",
                AccessFailedCount = 0,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                UserName = "UserName",
                NormalizedUserName = "USERNAME",
                Email = "simple@gmail.com",
                NormalizedEmail = "SIMPLE@gmail.com",
                PasswordHash = "Pasfjdsasd",
                ConcurrencyStamp = "fjjffjfjf",
                LockoutEnd = DateTime.Now,
                PhoneNumber = "885553531",
                SecurityStamp = "Securitystamp",
            };

            IStorytellerCRUD storytellerService = new StorytellerService();

            storytellerService.DeleteStorytellerById(storyteller.Id);

            Assert.IsNotNull(storyteller);
        }

        [TestMethod]
        public void CanGetStorytellerByValidId()
        {
            Storyteller storyteller = new Storyteller("Name", true, "img.png", "About", 18)
            {
                Id = "5050241f-a600-4d64-8634-0de904c043c1",
                AccessFailedCount = 0,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                UserName = "UserName",
                NormalizedUserName = "USERNAME",
                Email = "simple@gmail.com",
                NormalizedEmail = "SIMPLE@gmail.com",
                PasswordHash = "Pasfjdsasd",
                ConcurrencyStamp = "fjjffjfjf",
                LockoutEnd = DateTime.Now,
                PhoneNumber = "885553531",
                SecurityStamp = "Securitystamp",
            };

            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerCRUD storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            storytellerService1.CreateStoryteller(storyteller);
            var result = storytellerService2.GetStorytellerById(storyteller.Id);
            storytellerService3.DeleteStorytellerById(storyteller.Id);

            Assert.AreEqual(storyteller.Id, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void CannotGetStorytellerByInvalidId_ReturnException()
        {
            Storyteller storyteller = new Storyteller("Name", true, "img.png", "About", 18)
            {
                Id = null,
                AccessFailedCount = 0,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                UserName = "UserName",
                NormalizedUserName = "USERNAME",
                Email = "simple@gmail.com",
                NormalizedEmail = "SIMPLE@gmail.com",
                PasswordHash = "Pasfjdsasd",
                ConcurrencyStamp = "fjjffjfjf",
                LockoutEnd = DateTime.Now,
                PhoneNumber = "885553531",
                SecurityStamp = "Securitystamp",
            };

            IStorytellerCRUD storytellerService = new StorytellerService();

            storytellerService.GetStorytellerById(storyteller.Id);
        }

        [TestMethod]
        public void CanUpdateValidStoryteller_ReturnStoryteller()
        {
            Storyteller storyteller = new Storyteller("Name", true, "img.png", "About", 18)
            {
                Id = "5050241f-a600-4d64-8634-0de904c043c1",
                AccessFailedCount = 0,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                UserName = "UserName",
                NormalizedUserName = "USERNAME",
                Email = "simple@gmail.com",
                NormalizedEmail = "SIMPLE@gmail.com",
                PasswordHash = "Pasfjdsasd",
                ConcurrencyStamp = "fjjffjfjf",
                LockoutEnd = DateTime.Now,
                PhoneNumber = "885553531",
                SecurityStamp = "Securitystamp",
            };

            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerCRUD storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            storytellerService1.CreateStoryteller(storyteller);
            var result = storytellerService2.UpdateStoryteller(storyteller);
            storytellerService3.DeleteStorytellerById(storyteller.Id);

            Assert.AreEqual(storyteller, result.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CannotUpdateInvalidStoryteller_ReturnException()
        {
            Storyteller storyteller = null;

            IStorytellerCRUD storytellerService = new StorytellerService();

            await storytellerService.UpdateStoryteller(storyteller);
        }

        [TestMethod]
        public void CanFindStorytellerByValidId_ReturnStoryteller()
        {
            Storyteller storyteller = new Storyteller("Name", true, "img.png", "About", 18)
            {
                Id = "5050241f-a600-4d64-8634-0de904c043c1",
                AccessFailedCount = 0,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                UserName = "UserName",
                NormalizedUserName = "USERNAME",
                Email = "simple@gmail.com",
                NormalizedEmail = "SIMPLE@gmail.com",
                PasswordHash = "Pasfjdsasd",
                ConcurrencyStamp = "fjjffjfjf",
                LockoutEnd = DateTime.Now,
                PhoneNumber = "885553531",
                SecurityStamp = "Securitystamp",
            };

            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerService storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            storytellerService1.CreateStoryteller(storyteller);
            var result = storytellerService2.FindStorytellerById(storyteller.Id);
            storytellerService3.DeleteStorytellerById(storyteller.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanFindStorytellerByValidUserName_ReturnStoryteller()
        {
            Storyteller storyteller = new Storyteller("Name", true, "img.png", "About", 18)
            {
                Id = "5050241f-a600-4d64-8634-0de904c043c1",
                AccessFailedCount = 0,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                UserName = "UserName",
                NormalizedUserName = "USERNAME",
                Email = "simple@gmail.com",
                NormalizedEmail = "SIMPLE@gmail.com",
                PasswordHash = "Pasfjdsasd",
                ConcurrencyStamp = "fjjffjfjf",
                LockoutEnd = DateTime.Now,
                PhoneNumber = "885553531",
                SecurityStamp = "Securitystamp",
            };

            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerService storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            storytellerService1.CreateStoryteller(storyteller);
            var result = storytellerService2.FindStorytellersByUserName(storyteller.UserName);
            storytellerService3.DeleteStorytellerById(storyteller.Id);

            Assert.IsNotNull(result);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryHub.BL.Db_Context;
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

            // Seriously? Compare collections of objects?
            // For this it is worth using lambda expressions over each object.
            Assert.AreEqual(storytellers, result, "Created storytellers object and returned storytellers are not equal!");
        }

        [TestMethod]
        public void CanCreateValidStoryteller_ReturnId()
        {
            //Arrange
            Storyteller storyteller = new Storyteller("Name", 1, true, "img.png", "About", 18)
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

            //Act
            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerCRUD storytellerService2 = new StorytellerService();

            string result = storytellerService1.CreateStoryteller(storyteller).Result;
            storytellerService2.DeleteStorytellerById(storyteller.Id);

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(storyteller.Id), "Created Storyteller ID is Null Or White Space!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(result), "Returned Storyteller ID is Null Or White Space!");

            Assert.IsNotNull(result, "Storyteller method returned null!");
            Assert.IsNotNull(storyteller, "Created storyteller is null!");

            Assert.AreEqual(storyteller.Id, result, "Created storytellers id and returned storytellers id are not equal!");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CannotCreateStorytellerIfAlreadyExists_ReturnException()
        {
            //Arrange
            Storyteller storyteller = new Storyteller("Name", 1, true, "img.png", "About", 18)
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

            //Act
            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerCRUD storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            await storytellerService1.CreateStoryteller(storyteller);
            await storytellerService2.CreateStoryteller(storyteller);
            await storytellerService3.DeleteStorytellerById(storyteller.Id);

            //Assert
        }

        [TestMethod]
        public void CanDeleteStorytellerByValidId()
        {
            //Arrange
            Storyteller storyteller = new Storyteller("Name", 0, true, "img.png", "About", 18)
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

            //Act
            IStorytellerCRUD storytellerService = new StorytellerService();

            storytellerService.DeleteStorytellerById(storyteller.Id);

            //Assert
            Assert.IsNotNull(storyteller, "Created Storyteller is null!");
        }

        [TestMethod]
        public void CanGetStorytellerByValidId()
        {
            //Arrange
            Storyteller storyteller = new Storyteller("Name", 0, true, "img.png", "About", 18)
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

            //Act
            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerCRUD storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            storytellerService1.CreateStoryteller(storyteller);
            var result = storytellerService2.GetStorytellerById(storyteller.Id);
            storytellerService3.DeleteStorytellerById(storyteller.Id);

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(storyteller.Id), "Created Storyteller ID is Null Or White Space!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Id), "Returned Storyteller ID is Null Or White Space!");

            Assert.AreEqual(storyteller.Id, result.Id, "Created Storyteller ID and " +
                                                       "returned by GetStorytellerById method ID are not equal!");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void CannotGetStorytellerByInvalidId_ReturnException()
        {
            //Arrange
            Storyteller storyteller = new Storyteller("Name", 0, true, "img.png", "About", 18)
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

            //Act
            IStorytellerCRUD storytellerService = new StorytellerService();

            storytellerService.GetStorytellerById(storyteller.Id);

            //Assert
        }

        [TestMethod]
        public void CanUpdateValidStoryteller_ReturnStoryteller()
        {
            //Arrange
            Storyteller storyteller = new Storyteller("Name", 0, true, "img.png", "About", 18)
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

            //Act
            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerCRUD storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            storytellerService1.CreateStoryteller(storyteller);
            var result = storytellerService2.UpdateStoryteller(storyteller).Result;
            storytellerService3.DeleteStorytellerById(storyteller.Id);

            //Assert

            // Seriously? Compare classes by reference?
            /* I recommend adding an Equal method to the storyteller class and comparing its result.

                Assert.IsTrue(storyteller.Equal(result));
            */
            Assert.AreEqual(storyteller, result);
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
            //Arrange
            Storyteller storyteller = new Storyteller("Name", 0, true, "img.png", "About", 18)
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

            //Act
            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerService storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            storytellerService1.CreateStoryteller(storyteller);
            var result = storytellerService2.FindStorytellerById(storyteller.Id);
            storytellerService3.DeleteStorytellerById(storyteller.Id);


            //Assert
            Assert.IsNotNull(result, "FindStorytellerById method returned null!");
        }

        [TestMethod]
        public void CanFindStorytellerByValidUserName_ReturnStoryteller()
        {
            //Arrange
            Storyteller storyteller = new Storyteller("Name", 0, true, "img.png", "About", 18)
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

            //Act
            IStorytellerCRUD storytellerService1 = new StorytellerService();
            IStorytellerService storytellerService2 = new StorytellerService();
            IStorytellerCRUD storytellerService3 = new StorytellerService();

            storytellerService1.CreateStoryteller(storyteller);
            var result = storytellerService2.FindStorytellersByUserName(storyteller.UserName);
            storytellerService3.DeleteStorytellerById(storyteller.Id);

            //Assert
            Assert.IsNotNull(result, "FindStorytellersByUserName method returned null!");
        }
    }
}

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
    public class StoryServiceTest
    {
        // For cleanup.
        private readonly AppDbContext _appDbContext = new AppDbContext();

        private Story _story = new Story("Name",
                                         "Image.jpg",
                                         "Text.",
                                         0,
                                         0,
                                         null,
                                         null);
        [TestCleanup]
        public async Task TestCleanup()
        {
            if (_appDbContext.Stories.Find(_story.Id) != null)
            {
                StoryService storyService = new StoryService();
                await storyService.DeleteStoryById(_story.Id);
            }
        }

        [TestMethod]
        public void CreateStory_CanCreateValidStory_ReturnStoryId()
        {
            // Arrange.
            IStoryCRUD storyService1 = new StoryService();

            // Act.
            var result = storyService1.CreateStory(_story);

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(_story.Id, result.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateStory_CannotCreateIfStoryHave_ReturnException()
        {
            // Arrange.
            IStoryCRUD storyService1 = new StoryService();
            IStoryCRUD storyService2 = new StoryService();

            // Act.
            await storyService1.CreateStory(_story);
            await storyService2.CreateStory(_story);
        }

        [TestMethod]
        public async Task GetStory_CanGetStoryByValidId_ReturnStory()
        {
            // Arrange.
            IStoryCRUD storyService1 = new StoryService();
            IStoryCRUD storyService2 = new StoryService();

            // Act.
            await storyService1.CreateStory(_story);
            var result = storyService2.GetStoryById(_story.Id);

            // Assert.
            Assert.AreEqual(_story.Id, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetStory_CannotGetStoryIfNotFound_ReturnKeyNotFoundException()
        {
            // Arrange.
            IStoryCRUD storyService = new StoryService();

            // Act.
            storyService.GetStoryById(_story.Id);
        }
        [TestMethod]
        public async Task UpdateStory_CanUpdateStory()
        {
            // Arrange.
            IStoryCRUD storyService1 = new StoryService();
            IStoryCRUD storyService2 = new StoryService();

            // Act.
            await storyService1.CreateStory(_story);
            var result = storyService2.UpdateStory(_story);

            // Assert.
            Assert.AreEqual(_story.Id, result.Result.Id);
        }
    }
}

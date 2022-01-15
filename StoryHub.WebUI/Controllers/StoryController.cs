using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoryHub.BL.Models;
using StoryHub.BL.Services.Abstract;
using StoryHub.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryHub.WebUI.Controllers
{
    public class StoryController : Controller
    {
        private readonly ILogger<StoryController> _logger;
        private readonly IStoryService _storyService;
        private readonly IStoryCRUD _storyCRUD;
        private readonly IStorytellerCRUD _storytellerCRUD;
        private readonly IStorytellerService _storytellerService;

        public StoryController(
            ILogger<StoryController> logger,
            IStoryService storyService,
            IStoryCRUD storyCRUD,
            IStorytellerCRUD storytellerCRUD,
            IStorytellerService storytellerService)
        {
            _logger = logger;
            _storyService = storyService;
            _storyCRUD = storyCRUD;
            _storytellerCRUD = storytellerCRUD;
            _storytellerService = storytellerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var storyteller = await _storytellerService.GetCurrentStoryteller(HttpContext.User);
                _logger.LogInformation($"Finded current user: {storyteller.Name}");

                IEnumerable<Story> stories = await _storyService.GetStorytellerStories(storyteller);
                _logger.LogInformation("List is displayed to: " + storyteller.Name);

                return View(stories);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogInformation("User has no stories: " + ex.Message);
                return PartialView("NoStories");
            }
        }

        [HttpGet]
        public IActionResult AddStory()
        {
            var model = new StoryViewModel();
            return View(model);
        }

        [HttpPost, ActionName("AddStory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStoryPost(StoryViewModel storyViewModel)
        {
            if(ModelState.IsValid == false)
            {
                _logger.LogInformation("Data not valid.");
                return View(storyViewModel);
            }

            var storyteller = await _storytellerService.GetCurrentStoryteller(HttpContext.User);
            _logger.LogInformation($"Finded current user {storyteller.Name}");

            var story = new Story(storyteller.Id, storyViewModel.Name, null, storyViewModel.Text, null);
            await _storyCRUD.CreateStory(story);
            _logger.LogInformation($"Created story {story.Name} by {storyteller.Name}");

            storyteller.QuantityStories++;
            await _storytellerCRUD.UpdateStoryteller(storyteller);
            _logger.LogInformation($"Updated quantity stories at {storyteller.Name}");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateStory(string id)
        {
            try
            {
                var story = _storyCRUD.GetStoryById(id);
                _logger.LogInformation($"Finded story: {story.Name}");

                var model = new StoryViewModel()
                {
                    Id = story.Id,
                    Name = story.Name,
                    Text = story.Text,
                };

                return View(model);
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return NotFound(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogInformation($"Not founded story {id}: " + ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPost, ActionName("UpdateStory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStoryPost(StoryViewModel storyViewModel)
        {
            if (ModelState.IsValid == false)
            {
                _logger.LogInformation("Data not valid.");
                return View(storyViewModel);
            }

            var storyteller = await _storytellerService.GetCurrentStoryteller(HttpContext.User);
            _logger.LogInformation($"Finded current user {storyteller.Name}");

            var oldStory = _storyCRUD.GetStoryById(storyViewModel.Id);
            _logger.LogInformation($"Finded story {storyViewModel.Name}");

            var newStory = new Story(storyteller.Id, storyViewModel.Name, oldStory.Image, storyViewModel.Text, oldStory.Category)
            {
                Id = oldStory.Id,
                QuantityDislikes = oldStory.QuantityDislikes,
                QuantityLikes = oldStory.QuantityLikes,
            };

            await _storyCRUD.UpdateStory(newStory);
            _logger.LogInformation($"Updated current story {storyViewModel.Name}");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteStory(string id)
        {
            try
            {
                var story = _storyCRUD.GetStoryById(id);
                _logger.LogInformation($"Finded story {story.Name}");

                var model = new StoryViewModel()
                {
                    Id = story.Id,
                    Name = story.Name,
                    Text = story.Text,
                };

                return View(model);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return NotFound(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return NotFound(ex.Message);
            }
        }

        [HttpPost, ActionName("DeleteStory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStoryPost(StoryViewModel storyViewModel)
        {
            var storyteller = await _storytellerService.GetCurrentStoryteller(HttpContext.User);
            _logger.LogInformation($"Finded current user {storyteller.Name}");

            await _storyCRUD.DeleteStoryById(storyViewModel.Id);
            _logger.LogInformation($"Deleted story {storyViewModel.Name}");

            storyteller.QuantityStories--;
            await _storytellerCRUD.UpdateStoryteller(storyteller);
            _logger.LogInformation($"Updated quantity stories at {storyteller.Name}");

            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoryHub.BL.Models;
using StoryHub.BL.Services.Abstract;
using StoryHub.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryHub.WebUI.Controllers
{
    public class StorytellerManagerController : Controller
    {
        private readonly ILogger<StorytellerManagerController> _logger;
        private readonly IStorytellerService _storytellerService;
        private readonly IStorytellerCRUD _storytellerCRUD;

        public StorytellerManagerController(ILogger<StorytellerManagerController> logger,
                                            IStorytellerService storytellerService,
                                            IStorytellerCRUD storytellerCRUD)
        {
            _logger = logger;
            _storytellerService = storytellerService;
            _storytellerCRUD = storytellerCRUD;
        }

        public async Task<IActionResult> Index(string search)
        {
            IEnumerable<Storyteller> storytellers;
            if(string.IsNullOrWhiteSpace(search))
            {
                storytellers = await _storytellerService.GetAllStorytellers();
            }
            else
            {
                try
                {
                    storytellers = _storytellerService.FindStorytellersByUserName(search);
                } 
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    return NotFound(ex.Message);
                }
            }

            return View(storytellers);
        }

        public IActionResult UpdateStoryteller(string id)
        {
            Storyteller storyteller = _storytellerCRUD.GetStorytellerById(id);

            if (storyteller is null)
            {
                _logger.LogInformation("Storyteller not found.");
                return NotFound();
            }

            var updateStoryteller = new UpdateStorytellerViewModel
            {
                Id = storyteller.Id,
                About = storyteller.About,
                IsValidImage = true,
                OldStorytellerImage = storyteller.Image,
            };

            return View(updateStoryteller);
        }

        [HttpPost, ActionName("UpdateStoryteller")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStorytellerPost(UpdateStorytellerViewModel updateStoryteller)
        {
            if (ModelState.IsValid)
            {
                #region Initialization new storyteller
                Storyteller oldStoryteller = _storytellerCRUD.GetStorytellerById(updateStoryteller.Id);

                Storyteller newStoryteller = new Storyteller(oldStoryteller.Name,
                                                             oldStoryteller.QuantityStories,
                                                             oldStoryteller.Gender,
                                                             SetImage(oldStoryteller, updateStoryteller),
                                                             updateStoryteller.About,
                                                             oldStoryteller.Age)
                {
                    Id = oldStoryteller.Id,
                    UserName = oldStoryteller.UserName,
                    Email = oldStoryteller.Email,
                    LockoutEnd = oldStoryteller.LockoutEnd,
                    NormalizedEmail = oldStoryteller.NormalizedEmail,
                    NormalizedUserName = oldStoryteller.NormalizedUserName,
                    PasswordHash = oldStoryteller.PasswordHash,
                    PhoneNumber = oldStoryteller.PhoneNumber,
                };
                #endregion

                await _storytellerCRUD.UpdateStoryteller(newStoryteller);
                _logger.LogInformation($"Updated storyteller: {newStoryteller.Name}");
                return RedirectToAction("Index");
            }
            return View(updateStoryteller);
        }

        public IActionResult DeleteStoryteller(string id)
        {
            Storyteller storyteller = _storytellerService.FindStorytellerById(id);
            if (storyteller is null)
            {
                _logger.LogInformation("Storyteller not found.");
                return NotFound();
            }

            return PartialView(storyteller);
        }

        [HttpPost, ActionName("DeleteStoryteller")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStorytellerPost(string id)
        {
            await _storytellerCRUD.DeleteStorytellerById(id);
            _logger.LogInformation($"Deleted storyteller: {id}");
            return RedirectToAction("Index");
        }

        private string SetImage(Storyteller oldStoryteller, UpdateStorytellerViewModel updateStoryteller)
        {
            if (updateStoryteller.IsValidImage)
            {
                return oldStoryteller.Image;
            }

            return "defaultImageStoryteller.jpg";
        }
    }
}

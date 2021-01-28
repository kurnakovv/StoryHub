using Microsoft.AspNetCore.Mvc;
using StoryHub.BL.Models;
using StoryHub.BL.Services.Abstract;
using StoryHub.WebUI.ViewModels;

namespace StoryHub.WebUI.Controllers
{
    public class StorytellerManagerController : Controller
    {
        private readonly IStorytellerService _storytellerService;
        private readonly IStorytellerCRUD _storytellerCRUD;

        public StorytellerManagerController(IStorytellerService storytellerService,
                                            IStorytellerCRUD storytellerCRUD)
        {
            _storytellerService = storytellerService;
            _storytellerCRUD = storytellerCRUD;
        }

        public IActionResult Index()
        {
            var storytellers = _storytellerService.GetAllStorytellers();
            return View(storytellers);
        }

        public IActionResult UpdateStoryteller(string id)
        {
            Storyteller storyteller = _storytellerCRUD.GetStorytellerById(id);

            if (storyteller is null)
                return NotFound();

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
        public IActionResult UpdateStorytellerPost(UpdateStorytellerViewModel updateStoryteller)
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

                _storytellerCRUD.UpdateStoryteller(newStoryteller);
                return RedirectToAction("Index");
            }
            return View(updateStoryteller);
        }

        public IActionResult DeleteStoryteller(string id)
        {
            Storyteller storyteller = _storytellerService.FindStorytellerById(id);
            if (storyteller is null)
                return NotFound();

            return PartialView(storyteller);
        }

        [HttpPost, ActionName("DeleteStoryteller")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStorytellerPost(string id)
        {
            _storytellerCRUD.DeleteStorytellerById(id);
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

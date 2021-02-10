using Microsoft.AspNetCore.Mvc;
using StoryHub.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryHub.WebUI.Controllers
{
    public class StorytellerProfileController : Controller
    {
        private readonly IStorytellerService _storytellerCRUD;
        public StorytellerProfileController(IStorytellerService storytellerCRUD)
        {
            _storytellerCRUD = storytellerCRUD;
        }

        public IActionResult Index(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyteller = _storytellerCRUD.FindStorytellerById(id);
            return View(storyteller);
        }
    }
}

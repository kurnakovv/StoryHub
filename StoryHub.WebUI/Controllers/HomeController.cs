using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoryHub.BL.Models;
using StoryHub.BL.Services.Abstract;
using StoryHub.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StoryHub.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStorytellerService _storytellerService;
        private readonly IStorytellerCRUD _storytellerCRUD;

        public HomeController(ILogger<HomeController> logger,
                              IStorytellerService storytellerService,
                              IStorytellerCRUD storytellerCRUD)
        {
            _logger = logger;
            _storytellerService = storytellerService;
            _storytellerCRUD = storytellerCRUD;
        }

        public async Task<IActionResult> Index(string search)
        {
            IEnumerable<Storyteller> model;

            if (string.IsNullOrWhiteSpace(search))
            {
                model = await _storytellerService.GetAllStorytellers();
            }
            else
            {
                try
                {
                    model = _storytellerService.FindStorytellersByUserName(search);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

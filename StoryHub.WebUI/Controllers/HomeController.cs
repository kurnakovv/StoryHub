using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoryHub.BL.Services.Abstract;
using StoryHub.WebUI.Models;
using System.Diagnostics;

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

        public IActionResult Index()
        {
            var model = _storytellerService.GetAllStorytellers();
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

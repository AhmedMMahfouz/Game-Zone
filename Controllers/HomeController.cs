using System.Diagnostics;
using GameHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var x = _gameService.GetGames();
            return View("Index", x);
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

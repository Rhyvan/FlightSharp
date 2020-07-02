using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;

namespace FlightSharpWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SessionService _sessionService;

        public HomeController(ILogger<HomeController> logger, SessionService session)
        {
            _logger = logger;
            _sessionService = session;
        }

        public IActionResult Index()
        {
            var cart = _sessionService.GetSessionObject<Cart>("Cart");

            if (cart == null)
            {
                cart = new Cart();

                _sessionService.SetSessionString("userName", "anonym");
                _sessionService.SetSessionObject("Cart", cart);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search()
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

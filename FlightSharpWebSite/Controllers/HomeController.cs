using System.Diagnostics;
using System.Globalization;
using System.Linq;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            var localCurrency = RegionInfo.CurrentRegion.ISOCurrencySymbol;

            var currenciesISOsymbol = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture =>
                {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(region => region != null)
                .Select(ri => ri.ISOCurrencySymbol)
                .Distinct();

            ViewData["localCurrency"] = localCurrency;
            ViewData["currencies"] = currenciesISOsymbol;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

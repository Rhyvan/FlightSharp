using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using FlightSharpWebSite.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FlightSharpWebSite.Controllers
{
    [Route("api")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly SessionService _sessionService;

        public CartController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        // GET: api/<CartController>
        [HttpGet]
        public OkObjectResult Get()
        {
            var cart = HttpContext.Session.GetObject<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.SetString("userName", "anonym");
                HttpContext.Session.SetObject("Cart", cart);
            }


            return Ok("Session Data set");
        }

        /*[HttpPost("cart")]
        public HttpStatusCode addflight(Ticket ticket)
        {
            var cart = _sessionService.GetSessionObject<Cart>("Cart");

            if (cart == null)
            {
                return HttpStatusCode.BadRequest;
            }

            var flight = ticket.Flight;
            var quantity = ticket.Quantity;
            System.Console.WriteLine("This flight was: " + flight.AirLine + "  with price in HUF: " + flight.PriceHUF);

            if (!cart.AddToCart(flight, quantity))
            {
                // this actually could happen either due to server error or bad query
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }*/

        [HttpPost("OK")]
        public HttpStatusCode AddFlight(dynamic data)
        {
            var cart = _sessionService.GetSessionObject<Cart>("Cart");

            if (cart == null)
            {
                return HttpStatusCode.BadRequest;
            }

            try
            {
                var flight = data.GetProperty("Flight").ToString();
                var quantity = data.GetProperty("Quantity").GetInt32();

                // TODO ISSUE #1 The deserializer prevents any malformed input to enter.
                // So code in the if block below always returns true.
                // Need to add restrictions to the Cart class,
                // to have a minimum necessary properties.
                flight = JsonSerializer.Deserialize<Flight>(flight);

                if (!cart.AddToCart(flight, quantity))
                {
                    // this actually could happen either due to server error or bad query
                    return HttpStatusCode.InternalServerError;
                }
            }
            catch (System.Exception)
            {
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.OK;
        }

        [HttpPost("cart")]
        public async Task<IActionResult> WORK(dynamic data)
        {
            var flight = data.GetProperty("Flight").ToString();
            var quantity = data.GetProperty("Quantity").GetInt32();
            //var json = JObject.Parse(flight);

            /*var price = (string)flight.SelectToken("price");
            System.Console.WriteLine("price was: " +  price);*/
            System.Console.WriteLine("Flight is: " + flight);
            return NoContent();
        }
    }
}

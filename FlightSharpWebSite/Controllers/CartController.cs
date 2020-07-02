using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using FlightSharpWebSite.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace FlightSharpWebSite.Controllers
{
    [Route("Home/api")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly SessionService _sessionService;

        public CartController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        // GET: api/<CartController>
        [HttpGet]
        public ViewResult Get()
        {
            var cart = _sessionService.GetSessionObject<Cart>("Cart");
 
            if (cart == null)
            {
                cart = new Cart();

                //TODO SET these sessions in homepage!
                _sessionService.SetSessionString("userName", "anonym");
                _sessionService.SetSessionObject("Cart", cart);
            }

            ViewData["Cart"] = cart;
        
            return View("~/Views/Home/Cart.cshtml");
        }

        //[HttpPost]
        //public HttpStatusCode AddFlight(Ticket ticket)
        //{
        //    var cart = HttpContext.Session.GetObject<Cart>("Cart");
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        // return HttpStatusCode.BadRequest;
        //    }

        //    var flight = ticket.Flight;
        //    var quantity = ticket.Quantity;

        //    if (!cart.AddToCart(flight, quantity))
        //    {
        //        // this actually could happen either due to server error or bad query
        //        return HttpStatusCode.InternalServerError;
        //    }

        //    return HttpStatusCode.OK;
        //}  
        
        [HttpPost("")]
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

        [HttpPost("Cart")]
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

using System.Net;
using System.Text.Json;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using FlightSharpWebSite.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FlightSharpWebSite.Controllers
{
    [Route("api/[controller]")]
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
    }
}

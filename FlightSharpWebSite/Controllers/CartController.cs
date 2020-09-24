using System;
using System.Collections.Generic;
using System.Text.Json;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("cart")]
        public IActionResult AddFlight(dynamic data)
        {
            var cart = _sessionService.GetSessionObject<Cart>("Cart");

            if (cart == null)
            {
                return NotFound();
            }

            try
            {
                var flight = data.GetProperty("Flight").ToString();
                var quantity = data.GetProperty("Quantity").GetInt32();

                // TODO ISSUE #1 The deserializer prevents any malformed input to enter.
                // So code in the if block below always returns true.
                // Need to add restrictions to the Cart class,
                // to have a minimum necessary properties.
                flight = JsonSerializer.Deserialize<Flight>(flight, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!cart.AddToCart(flight, quantity))
                {
                    // this actually could happen either due to server error or bad query
                    return StatusCode(500);
                }
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }

            _sessionService.SetSessionObject("Cart", cart);
            return Ok();
        }

        [HttpPost("delete")]
        public IActionResult DeleteFlights(dynamic data)
        {
            var cart = _sessionService.GetSessionObject<Cart>("Cart");

            if (cart == null)
            {
                return NotFound();
            }

            try
            {
                var flight = data.GetProperty("Flight").ToString();
                flight = JsonSerializer.Deserialize<Flight>(flight);
                cart.DeleteFromCart(flight);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            _sessionService.SetSessionObject("Cart", cart);

            return Ok();
        }
    }
}

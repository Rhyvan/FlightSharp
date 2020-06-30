using FlightSharpWebSite.Models;
using FlightSharpWebSite.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightSharpWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
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
    }
}

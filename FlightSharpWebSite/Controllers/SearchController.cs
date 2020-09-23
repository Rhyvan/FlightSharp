using System.Collections.Generic;
using System.Linq;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightSharpWebSite.Controllers
{
    [ApiController]
    [Route("Home/api")]
    public class SearchController : ControllerBase
    {
        private IFlightApiService _flightApiService;

        public SearchController(IFlightApiService flightApiService)
        {
            _flightApiService = flightApiService;
        }


        [HttpGet("search")]
        public ActionResult<IEnumerable<Flight>> GetSearchedFlights([FromQuery] SearchFlight query)
        {
            var flights = _flightApiService.GetFlights(
                query.Origin,
                query.Destination,
                query.Currency,
                query.MaxPrice
                );

            if (flights == null)
            {
                return NoContent();
            }

            return Ok(flights.ToList());
        }
    }
}

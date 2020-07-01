using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightSharpWebSite.Controllers
{
    [ApiController]
    [Route("api")]
    public class APIController : ControllerBase
    {
        private ClientService clientService;
        private FlightApiService flightApiService;
        public APIController()
        {
            clientService = new ClientService();
            flightApiService = new FlightApiService(clientService);
        }


        [HttpGet("search")]
        public ActionResult<IEnumerable<Flight>> GetSearchedFlights(string origin, string destination)
        {
            IEnumerable<Flight> flights = flightApiService.GetFlights(origin, destination);
            return flights.ToList();

        }
    }
}

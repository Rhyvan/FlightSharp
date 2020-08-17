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
    [Route("Home/api")]
    public class APIController : ControllerBase
    {
        public List<Flight> currentFlights = new List<Flight>();
        private ClientService clientService;
        private FlightApiService flightApiService;
        public APIController()
        {
            clientService = new ClientService();
            flightApiService = new FlightApiService(clientService);
        }


        [HttpGet("search")]
        public ActionResult<IEnumerable<Flight>> GetSearchedFlights([FromQuery]string origin, [FromQuery]string destination, [FromQuery]int price)
        {
            currentFlights.Clear();
            IEnumerable<Flight> flights = flightApiService.GetFlights(origin, destination, price);
            foreach (var flight in flights)
            {
                currentFlights.Add(flight);
                Console.WriteLine(flight);
            }
            return flights.ToList();
        }
    }
}

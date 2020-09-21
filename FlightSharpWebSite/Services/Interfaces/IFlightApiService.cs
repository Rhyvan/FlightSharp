using System.Collections.Generic;
using FlightSharpWebSite.Models;

namespace FlightSharpWebSite.Services
{
    public interface IFlightApiService
    {
        public IEnumerable<Flight> GetFlights(string origin, string destination, int maxPrice);
        public IEnumerable<Flight> GetFlights(string origin, string destination, string currency, int maxPrice);

    }
}

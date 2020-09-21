using System.Collections.Generic;
using System.Linq;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlightSharpWebSite
{
    public class FlightApiService : IFlightApiService
    {
        private IClientService _client;


        public FlightApiService(IClientService clientService)
        {
            _client = clientService;
        }


        public virtual IEnumerable<Flight> GetFlights(string origin, string destination, int price)
        {
            var resp = _client.GetFlights(origin, destination, "HUF");
            var json = JObject.Parse(resp);
            var flightsJson = json["data"][destination].ToString();
            IEnumerable<Flight> flights = JsonConvert.DeserializeObject<Dictionary<string, Flight>>(flightsJson)
                .Select(kvp => kvp.Value);
            foreach (var flight in flights)
            {
                flight.Origin = origin;
                flight.Destination = destination;
            }
            if (price > 0)
            {
                return from flight in flights
                       where flight.PriceHUF < price
                       select flight;
            }
            return flights;
        }


        public IEnumerable<Flight> GetFlights(string origin, string destination, string currency, int maxPrice)
        {
            var resp = _client.GetFlights(origin, destination, currency);
            var json = JObject.Parse(resp);
            var flightsJson = json["data"][destination].ToString();
            IEnumerable<Flight> flights = JsonConvert.DeserializeObject<Dictionary<string, Flight>>(flightsJson)
                .Select(kvp => kvp.Value);
            return from flight in flights
                   where flight.PriceHUF < maxPrice
                   select flight;
        }
    }
}
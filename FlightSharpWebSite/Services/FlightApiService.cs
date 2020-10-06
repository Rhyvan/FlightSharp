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
        private readonly IClientService _client;


        public FlightApiService(IClientService clientService)
        {
            _client = clientService;
        }

        public IEnumerable<Flight> GetFlights(string origin, string destination, string currency, int maxPrice = 0)
        {
            var resp = _client.GetFlights(origin, destination, currency);
            var json = JObject.Parse(resp);
            var data = json["data"][destination];
            if ( data == null)
            {
                return null;
            }

            var flights = JsonConvert.DeserializeObject<Dictionary<string, Flight>>(data.ToString())
                .Select(kvp => kvp.Value);

            foreach (var flight in flights)
            {
                flight.Origin = origin;
                flight.Destination = destination;
                flight.Currency = currency;
            }

            if (maxPrice != 0)
            {
                return from flight in flights
                    where flight.Price < maxPrice
                    select flight;
            }

            return flights;
        }
    }
}
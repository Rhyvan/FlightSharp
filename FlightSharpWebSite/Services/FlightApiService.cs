using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FlightSharpWebSite
{
    public class FlightApiService : IFlightApiService
    {
        private IClientService _client;


        public FlightApiService(IClientService clientService)
        {
            _client = clientService;
        }


        public virtual IEnumerable<Flight> GetFlights(string origin, string destination)
        {
            var resp = _client.GetFlights(origin, destination, "HUF");
            var json = JObject.Parse(resp);
            var flightsJson = json["data"][destination].ToString();
            IEnumerable<Flight> flights = JsonConvert.DeserializeObject<Dictionary<string, Flight>>(flightsJson)
                .Select(kvp => kvp.Value);
            return flights;
        }

        public IEnumerable<Flight> GetFlights(string origin, string destination, string currency)
        {
            var resp = _client.GetFlights(origin, destination, currency);
            var json = JObject.Parse(resp);
            var flightsJson = json["data"][destination].ToString();
            IEnumerable<Flight> flights = JsonConvert.DeserializeObject<Dictionary<string, Flight>>(flightsJson)
                .Select(kvp => kvp.Value);
            return flights;
        }

        public IEnumerable<Flight> GetFlights(string origin, string destination, string currency, int maxPrice)
        {
            var resp = _client.GetFlights(origin, destination, currency);
            var json = JObject.Parse(resp);
            var flightsJson = json["data"][destination].ToString();
            IEnumerable<Flight> flights = JsonConvert.DeserializeObject<Dictionary<string, Flight>>(flightsJson)
                .Select(kvp => kvp.Value);
            return from flight in flights where flight.PriceHUF <= maxPrice select flight;
        }
    }

    public interface IFlightApiService
    {
        public IEnumerable<Flight> GetFlights(string origin, string destination);
        public IEnumerable<Flight> GetFlights(string origin, string destination, string currency);
        public IEnumerable<Flight> GetFlights(string origin, string destination, string currency, int maxPrice);

    }
}
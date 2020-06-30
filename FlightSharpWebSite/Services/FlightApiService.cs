using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
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
            //var resp = GetResponseAsString(origin, destination);
            var resp = _client.GetFlights(origin, destination, "HUF");
            var json = JObject.Parse(resp);
            var flightsJson = json["data"][destination].ToString();
            IEnumerable<Flight> flights = JsonConvert.DeserializeObject<Dictionary<string, Flight>>(flightsJson)
                .Select(kvp => kvp.Value);


            return flights;
        }
    }
}
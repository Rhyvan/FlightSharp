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


        public string GetResponseAsString(string origin, string destination)
        {
            string baseUrl =
                "https://travelpayouts-travelpayouts-flight-data-v1.p.rapidapi.com/v1/prices/cheap?destination={0}&origin={1}&currency=HUF&page=None";
            string url = string.Format(baseUrl, destination, origin);
            _restClient = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-access-token", "237f37871102101c4ec439ba6c98520e");
            request.AddHeader("x-rapidapi-host", "travelpayouts-travelpayouts-flight-data-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "22f98697d5mshbe41e4f988558c4p113d77jsnb304fc8f16d7");
            IRestResponse response = _restClient.Execute(request);
            string cont = response.Content;
            return cont;
        }
    }

    public interface IFlightApiService
    {
        public string GetResponseAsString(string origin, string destination);
        public IEnumerable<Flight> GetFlights(string origin, string destination);
    }
}
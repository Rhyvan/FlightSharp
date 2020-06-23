using FlightSharpWebSite.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;

namespace FlightSharpWebSite
{
    public class ApiService
    {
        public int FlightsCounted { get; private set; }
        public ApiService()
        {

        }

        public void GetOneFlightFromFlights(string destination, int flightCounter, Flight currentFlight)
        {
            string resp = GetResponseAsString(destination);
            int numOfFlights = CountResults(resp);
            var json = JObject.Parse(resp);
            SetFlight(currentFlight, destination, flightCounter, json);
        }

        private string GetResponseAsString(string destination)
        {
            string baseUrl = "https://travelpayouts-travelpayouts-flight-data-v1.p.rapidapi.com/v1/prices/cheap?destination={0}&origin=BUD&currency=HUF&page=None";
            string url = string.Format(baseUrl, destination);
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-access-token", "237f37871102101c4ec439ba6c98520e");
            request.AddHeader("x-rapidapi-host", "travelpayouts-travelpayouts-flight-data-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "22f98697d5mshbe41e4f988558c4p113d77jsnb304fc8f16d7");
            IRestResponse response = client.Execute(request);
            string cont = response.Content;

            return cont;
        }

        private int CountResults(string resp)
        {
            return resp.Count(f => f == '{') - 3;
        }

        private void SetFlight(Flight current, string airport, int num, JObject jObj)
        {
            current.Destination = airport;

            string selectToken = "data.{0}.";
            selectToken = string.Format(selectToken, airport);

            int fields = 5;

            for (int i = 0; i < fields; i++)
            {
                current.PriceHUF = (string)jObj.SelectToken(selectToken + i + ".price");
                current.AirLine = (string)jObj.SelectToken(selectToken + i + ".airline");
                current.FlightNo = (string)jObj.SelectToken(selectToken + i + ".flight_num");
                current.Departure = (string)jObj.SelectToken(selectToken + i + ".departure_at");
                current.Return = (string)jObj.SelectToken(selectToken + i + ".return_at");
            }

        }
    }
}

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

            string selectToken = "data.{0}.{1}.";
            string baseToken = string.Format(selectToken, airport, num.ToString());
            int fields = 5;

            for (int i = 0; i < fields; i++)
            {
                if (i == 0)
                {
                    string currentToken = baseToken + "price";
                    current.PriceHUF = (string)jObj.SelectToken(currentToken);
                }
                else if (i == 1)
                {
                    string currentToken = baseToken + "airline";
                    current.AirLine = (string)jObj.SelectToken(currentToken);
                }
                else if (i == 2)
                {
                    string currentToken = baseToken + "flight_number";
                    current.FlightNo = (string)jObj.SelectToken(currentToken);
                }
                else if (i == 3)
                {
                    string currentToken = baseToken + "departure_at";
                    current.Departure = (string)jObj.SelectToken(currentToken);
                }
                else if (i == 4)
                {
                    string currentToken = baseToken + "return_at";
                    current.Return = (string)jObj.SelectToken(currentToken);
                }
            }

        }
    }
}

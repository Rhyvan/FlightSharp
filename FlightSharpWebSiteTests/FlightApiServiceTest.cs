using NUnit.Framework;
using FlightSharpWebSite;
using NSubstitute;
using NSubstitute.Extensions;
using System.Linq;
using FlightSharpWebSite.Models;
using System;
using RestSharp;
using FlightSharpWebSite.Services;

namespace FlightSharpWebSiteTests
{
    public class FlightApiServiceTest
    {
        FlightApiService flightApi;
        IClientService clientService;
        string origin = "BUD";
        string destination = "DUB";
        int price = 53731;
        string currency = "HUF";
        int maxPrice = 200000;
        string airLine = "FR";
        int flightNo = 1024;
        string departureTime = "2020 - 08 - 03T17: 25:00Z";
        string returnTime = "2020 - 08 - 07T23: 20:00Z";
        string expireTime = "2020 - 07 - 01T15: 50:13Z";

        [OneTimeSetUp]
        public void Setup()
        {
            clientService = Substitute.For<IClientService>();
            flightApi = new FlightApiService(clientService);
        }


        [Test]
        public void TestGetFlightsFromTo()
        {

            var response = $@"{{
                ""success"": true,
                ""data"":
                {{
                    ""DUB"": 
                    {{
                        ""0"": 
                        {{
                            ""price"": ""{price}"",
                            ""airline"": ""{airLine}"",
                            ""flight_number"": ""{flightNo}"",
                            ""departure_at"": ""{departureTime}"",
                            ""return_at"": ""{returnTime}"",
                            ""expires_at"": ""{expireTime}""
                        }}
                    }}
                }},
                ""error"": ""null"",
                ""currency"":""{currency}""}}";

            var expected = new Flight()
            {
                PriceHUF = price,
                AirLine = airLine,
                FlightNo = flightNo,
                Departure = DateTime.Parse(departureTime).ToUniversalTime(),
                Return = DateTime.Parse(returnTime).ToUniversalTime(),
                ExpirationDate = DateTime.Parse(expireTime).ToUniversalTime()
            };
           
            clientService.GetFlights("BUD", destination, "HUF").Returns(response);

            var result = flightApi.GetFlights(origin, destination, currency, maxPrice);
            
            Assert.AreEqual(expected.ToJson(), result.First().ToJson());
        }

        [Test]
        public void TestGetFlights_HandleEmptyDataInResponse()
        {
            var response = @"{
                ""success"": true,
                ""data"": {},
                ""error"": ""null"",
                }";

            clientService.GetFlights(origin, destination, currency).Returns(response);
            var result = flightApi.GetFlights(origin, destination, currency, maxPrice);

            Assert.IsNull(result);

        }
    }
}
using NUnit.Framework;
using FlightSharpWebSite;
using NSubstitute;
using NSubstitute.Extensions;
using System.Linq;
using FlightSharpWebSite.Models;
using System;
using RestSharp;

namespace FlightSharpWebSiteTests
{
    public class Tests
    {
        FlightApiService flightApi;
        IRestClient client;
        IRestRequest request;
        IRestResponse response;
        string responseResult = @"{'success':true,'data':{'DUB':{'0':{'price':53731,'airline':'FR','flight_number':1024,'departure_at':'2020 - 08 - 03T17: 25:00Z','return_at':'2020 - 08 - 07T23: 20:00Z','expires_at':'2020 - 07 - 01T15: 50:13Z'}}},'error':null,'currency':'HUF'}";

        [OneTimeSetUp]
        public void Setup()
        {
            client = Substitute.For<IRestClient>();
            request = Substitute.For<IRestRequest>();
            response = Substitute.For<IRestResponse>();
            response.Content = responseResult;
            flightApi = new FlightApiService(client);
        }

        [Test]
        public void TestGetAllFlightWithDependencyInjection()
        {
            client.Execute(request).Returns(response);

            var expected = new Flight()
            {
                PriceHUF = 53731,
                AirLine = "FR",
                FlightNo = 1024,
                Departure = DateTime.Parse("2020 - 08 - 03T17: 25:00Z").ToUniversalTime(),
                Return = DateTime.Parse("2020 - 08 - 07T23: 20:00Z").ToUniversalTime(),
            };

            var result = flightApi.GetFlights("BUD", "DUB");

            Assert.AreEqual(expected.ToJson(), result.First().ToJson());
        }


        [Test]
        public void TestGetFlightsFromTo()
        {
            var destination = "DUB";
            var price = 53731;
            var airLine = "FR";
            var flightNo = 1024;
            var departureTime = "2020 - 08 - 03T17: 25:00Z";
            var returnTime = "2020 - 08 - 07T23: 20:00Z";
            var expireTime = "2020 - 07 - 01T15: 50:13Z";

            // TODO which response variable to use? 
            //string response = @"{'success':true,'data':{'DUB':{'0':{'price':53731,'airline':'FR','flight_number':1024,'departure_at':'2020 - 08 - 03T17: 25:00Z','return_at':'2020 - 08 - 07T23: 20:00Z','expires_at':'2020 - 07 - 01T15: 50:13Z'}}},'error':null,'currency':'HUF'}";
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
                ""currency"":""HUF""}}";

            var expected = new Flight()
            {
                PriceHUF = price,
                AirLine = airLine,
                FlightNo = flightNo,
                Departure = DateTime.Parse(departureTime).ToUniversalTime(),
                Return = DateTime.Parse(returnTime).ToUniversalTime(),
            };

            var apiServiceStub = Substitute.ForPartsOf<FlightApiService>();
            apiServiceStub.Configure().GetResponseAsString(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(response);

            var result = apiServiceStub.GetFlights("BUD", destination);
            
            Assert.AreEqual(expected.ToJson(), result.First().ToJson());
        }
    }
}
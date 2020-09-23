using System.Collections.Generic;
using FlightSharpWebSite.Controllers;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace FlightSharpWebSiteTests
{
    [TestFixture]
    class SearchControllerTest
    {
        private SearchController _controller;
        private IFlightApiService _flightApiService;

        public SearchControllerTest()
        {
            _flightApiService = Substitute.For<IFlightApiService>();
            _controller = new SearchController(_flightApiService);
        }


        [Test]
        public void TestGetSearchedFlights_QueryReturnsNoData()
        {
            var origin = "a";
            var destination = "b";
            var currency = "USD";
            var maxPrice = 1000;

            _flightApiService.GetFlights(origin, destination, currency, maxPrice).ReturnsNull();

            //var dictionary = new Dictionary<string, StringValues>();
            //dictionary.Add("origin", new StringValues("DUB"));
            //dictionary.Add("destination", new StringValues("BUD"));
            //_controller.Request.Query = new QueryCollection(dictionary);

            var query = new SearchFlight()
            {
                Origin = origin,
                Destination = destination,
                MaxPrice = maxPrice,
                Currency = currency
            };
            var result = _controller.GetSearchedFlights(query);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result.Result);
        }
    }
}

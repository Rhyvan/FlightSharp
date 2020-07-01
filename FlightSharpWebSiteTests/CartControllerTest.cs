using System.Net;
using FlightSharpWebSite.Controllers;
using FlightSharpWebSite.Models;
using FlightSharpWebSite.Services;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using System.Text.Json;

namespace FlightSharpWebSiteTests
{
    [TestFixture]
    class CartControllerTest
    {

        CartController controller;
        SessionService sessionService;
        const string DATA = "{ \"Flight\": { \"price\": 5000, \"airline\": \"LOT\"},\"Quantity\": 1}";


        [SetUp]
        public void SetUpBeforeAllTest()
        {
            var httpAccessor = Substitute.For<IHttpContextAccessor>();
            sessionService = Substitute.ForPartsOf<SessionService>(httpAccessor);
            controller = new CartController(sessionService);
        }

        [Test]
        public void POST_AddFlightTest_HasSession_CorrectJSon()
        {
            var cart = new Cart();
            var key = "Cart";

            sessionService.Configure().GetSessionObject<Cart>(key).Returns(cart);

            using (var jsonDoc = JsonDocument.Parse(DATA)) {
                JsonElement root = jsonDoc.RootElement;

                var result = controller.AddFlight(root);

                Assert.That(HttpStatusCode.OK == result);
            } 
        }

        [Test]
        public void POST_AddFlightTest_NoSession()
        {
            using (var jsonDoc = JsonDocument.Parse(DATA))
            {
                JsonElement root = jsonDoc.RootElement;

                var result = controller.AddFlight(root);

                Assert.That(HttpStatusCode.BadRequest == result);
            }
        }

        [TestCase("randomstring")]
        [TestCase("{ \"Flight\": \"badtext\",\"Quantity\": 1}")]
        public void POST_AddFlightTest_WrongDataInput(string input)
        {
            var result = controller.AddFlight(input);

            Assert.That(HttpStatusCode.BadRequest == result);
        }
        
        [Test]
        [Ignore("Until Issue in CartController is not fixed, will not reach its destination")]
        public void POST_AddFlightTest_HasSession_ServerError()
        {
            var cart = new Cart();
            var key = "Cart";
            var partialData = "{ \"Flight\": { \"airline\" : \"LOT\"},\"Quantity\": 1}";

            sessionService.Configure().GetSessionObject<Cart>(key).Returns(cart);

            using (var jsonDoc = JsonDocument.Parse(partialData))
            {
                JsonElement root = jsonDoc.RootElement;

                var result = controller.AddFlight(root);

                Assert.That(HttpStatusCode.InternalServerError == result);
            }
        }

    }
}

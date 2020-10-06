using System.Collections.Generic;
using System.Collections.ObjectModel;
using FlightSharpWebSite.Models;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace FlightSharpWebSiteTests
{
    [TestFixture]
    class CartTest
    {
        Cart _cart;
        Flight flightA;
        Flight flightB;

        [OneTimeSetUp]
        public void SetUpBeforeAllTest()
        {
            flightA = new Flight()
            {
                Price = 1000,
                FlightNo = 123,
                AirLine = "MALEV"
            };

            flightB = new Flight()
            {
                Price = 1000,
                FlightNo = 123,
                AirLine = "MALEV"
            };
        }

        [SetUp]
        public void SetupBeforeEach()
        {
            _cart = new Cart();
        }

        [Test]
        public void AddToCart_AddExisting()
        {
            _cart.AddToCart(flightA, 1);
            _cart.AddToCart(flightA, 1);

            Assert.That(_cart.Tickets[0].Quantity == 2 && _cart.Tickets.Count == 1);
        }

        [Test]
        public void AddToCart_AddNewItem()
        {
            _cart.AddToCart(flightA, 1);
            var flightC = new Flight() { Price = 500, FlightNo = 123, AirLine = "LOT" };

            _cart.AddToCart(flightC, 1);

            Assert.That(_cart.Tickets.Count == 2);
        }

        [Test]
        public void IsIncart_ReturnFalse()
        {
            Assert.IsFalse(_cart.IsInCart(flightA));
        }

        [Test]
        [Description("The method tests for IEquatable equality")]
        public void IsInCart_ReturnsTrue()
        {
            _cart.AddToCart(flightA, 1);

            Assert.IsTrue(_cart.IsInCart(flightB));
        }

        [Test]
        public void DeleteFromCartTest()
        {
            Assert.That(_cart.Tickets.Count == 0);
            
            _cart.AddToCart(flightA, 1);

            Assert.That(_cart.Tickets.Count == 1);

            _cart.DeleteFromCart(flightA);

            Assert.That(_cart.Tickets.Count == 0);
        }
    }
}

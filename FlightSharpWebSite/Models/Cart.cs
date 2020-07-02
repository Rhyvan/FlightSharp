using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlightSharpWebSite.Models
{
    public class Cart
    {
        public List<Flight> BookedFlights;

        public List<Ticket> Tickets { get; set; }


        public Cart()
        {
            Tickets = new List<Ticket>();
        }

        public bool AddToCart(Flight flight, int quantity)
        {
            try
            {
                if (IsInCart(flight))
                {
                    UpdateQuantity(flight, quantity);
                }
                else
                {
                    Tickets.Add(new Ticket(flight, quantity));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public void DeleteFromCart(Flight flight)
        {
            Tickets.RemoveAll(x => x.Flight.Equals(flight));
        }

        public void UpdateQuantity(Flight flight, int quantity)
        {
            Tickets.Where(x => x.Flight.Equals(flight))
                .Select(x => x)
                .FirstOrDefault()
                .Quantity += quantity;
        }

        public bool IsInCart(Flight flight)
        {
            return Tickets.Any(x => x.Flight.Equals(flight));
        }
    }
}
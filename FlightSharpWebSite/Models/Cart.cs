using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlightSharpWebSite.Models
{
    public class Cart
    {
        public List<Flight> BookedFlights;

        List<Ticket> _Tickets;

        public ReadOnlyCollection<Ticket> Tickets { get; set; } //cannot deserialize...

        public Cart()
        {
            _Tickets = new List<Ticket>();
            Tickets = _Tickets.AsReadOnly();
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
                    _Tickets.Add(new Ticket(flight, quantity));
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
           
            _Tickets.RemoveAll(x => x.Flight.Equals(flight));
        }

        public void UpdateQuantity(Flight flight, int quantity)
        {
            _Tickets.Where(x => x.Flight.Equals(flight))
                .Select(x => x)
                .FirstOrDefault()
                .Quantity += quantity;
        }

        public bool IsInCart(Flight flight)
        {
            return _Tickets.Any(x => x.Flight.Equals(flight));
        }
    }
}

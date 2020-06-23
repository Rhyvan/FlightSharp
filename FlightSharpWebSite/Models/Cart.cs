using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSharpWebSite.Models
{
    public class Cart
    {
        public List<Flight> BookedFlights;
        public User User { get; set; }

        public Cart(User user)
        {
            this.User = user;
        }
    }
}

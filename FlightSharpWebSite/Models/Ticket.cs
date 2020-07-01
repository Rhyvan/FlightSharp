using Newtonsoft.Json;

namespace FlightSharpWebSite.Models
{
    public class Ticket
    {
        [JsonProperty("Quantity")]
        public int Quantity { get; set; } = 1;

        [JsonProperty("Flight")]
        public Flight Flight { get; set; }

        public Ticket(Flight flight, int quantity)
        {
            Flight = flight;
            Quantity = quantity;
        }
        public Ticket()
        {

        }
    }
}

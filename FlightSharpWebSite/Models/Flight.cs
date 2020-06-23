using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightSharpWebSite.Models
{
    public class Flight
    {
        public string Return { get; set; }
        public string PriceHUF { get; set; }
        public string Destination { get; set; }
        public string Departure { get; set; }
        public string FlightNo { get; set; }

        public string AirLine { get; set; }


        public Flight()
        {

        }

        public override string ToString() => "Flight Number: " + FlightNo + " Date: " + Return + " Start: " + Departure + " Destination: " +
                                             Destination + " Price in HUF: " + PriceHUF;


        public string ToJson() => JsonSerializer.Serialize<Flight>(this);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightSharpWebSite.Models
{
    public class Flight
    {
        public string Date { get; set; }
        public int PriceHUF { get; set; }
        public string Destination { get; set; }
        public string Start { get; set; }
        public int FlightNo { get; set; }

        public Flight(string date, int priceHuf, string destination, string start, int flightNo)
        {
            Date = date;
            PriceHUF = priceHuf;
            Destination = destination;
            Start = start;
            FlightNo = flightNo;
        }

        public override string ToString() => "Flight Number: " + FlightNo + " Date: " + Date + " Start: " + Start + " Destination: " +
                                             Destination + " Price in HUF: " + PriceHUF;


        public string ToJson() => JsonSerializer.Serialize<Flight>(this);
    }
}
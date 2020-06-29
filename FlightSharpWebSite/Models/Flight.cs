using System;
using Newtonsoft.Json;

namespace FlightSharpWebSite.Models
{
    public class Flight
    {
        [JsonProperty("return_at")]
        public DateTime Return { get; set; }

        [JsonProperty("price")]
        public int PriceHUF { get; set; }

        public string Destination { get; set; }

        [JsonProperty("departure_at")]
        public DateTime Departure { get; set; }

        [JsonProperty("flight_number")]
        public int FlightNo { get; set; }

        [JsonProperty("airline")]
        public string AirLine { get; set; }

        public Flight()
        {

        }

        public override string ToString() => "Flight Number: " + FlightNo + " Date: " + Return + " Start: " + Departure + " Destination: " +
                                             Destination + " Price in HUF: " + PriceHUF;


        public string ToJson() => JsonConvert.SerializeObject(this); //System.Text.Json.JsonSerializer.Serialize<Flight>(this);
    }
}
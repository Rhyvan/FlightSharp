using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FlightSharpWebSite.Models
{
    public class Flight : IEquatable<Flight>
    {
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd hh:mm}")]
        [JsonProperty("return_at")]
        public DateTime Return { get; set; }

        [JsonProperty("price")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int PriceHUF { get; set; }

        public string Origin { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd hh:mm}")]
        [JsonProperty("departure_at")]
        public DateTime Departure { get; set; }

        [JsonProperty("flight_number")]
        [Required]
        public int FlightNo { get; set; }

        [JsonProperty("airline")]
        public string AirLine { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd hh:mm}")]
        [JsonProperty("expires_at")]
        public DateTime ExpirationDate { get; set; }

        public Flight()
        {

        }

        public override string ToString() => "Flight Number: " + FlightNo + " Date: " + Return + " Start: " + Departure + " Destination: " +
                                             Destination + " Origin: " + Origin + " Price in HUF: " + PriceHUF;


        public string ToJson() => JsonConvert.SerializeObject(this); //System.Text.Json.JsonSerializer.Serialize<Flight>(this);


        public bool Equals(Flight other)
        {
            if (other == null) return false;
            return (Return, PriceHUF, Origin, Destination, Departure, FlightNo, AirLine, ExpirationDate) ==
                (other.Return,
                other.PriceHUF,
                other.Origin,
                other.Destination,
                other.Departure,
                other.FlightNo,
                other.AirLine,
                other.ExpirationDate);
        }
    }
}
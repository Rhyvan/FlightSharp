using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FlightSharpWebSite.Models
{
    [BindProperties(SupportsGet = true)]
    public class SearchFlight
    {
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        [StringLength(3)]
        public string Currency { get; set; } = "USD";
        [BindProperty(Name = "price")]
        public int MaxPrice { get; set; }
    }
}

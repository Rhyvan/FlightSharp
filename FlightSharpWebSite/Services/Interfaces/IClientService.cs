using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSharpWebSite.Services
{
    public interface IClientService
    {
        public string GetFlights(string origin, string destination, string currency);
    }
}

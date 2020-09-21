namespace FlightSharpWebSite.Services
{
    public interface IClientService
    {
        public string GetFlights(string origin, string destination, string currency);
    }
}

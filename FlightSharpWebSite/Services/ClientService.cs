using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using RestSharp;

namespace FlightSharpWebSite.Services
{
    public class ClientService : IClientService
    {
        private string _baseUrl = "https://travelpayouts-travelpayouts-flight-data-v1.p.rapidapi.com/v1/prices";
        private IRestRequest _request;
        private IRestClient _restClient;

        public ClientService()
        {
            _restClient = new RestClient(_baseUrl);
        }

        public string GetFlights(string origin, string destination, string currency)
        {
            _request = new RestRequest("cheap", Method.GET);
            _request.AddParameter("destination", destination, ParameterType.QueryString);
            _request.AddParameter("origin", origin, ParameterType.QueryString);
            _request.AddParameter("currency", currency, ParameterType.QueryString);
            _request.AddParameter("page", "none", ParameterType.QueryString);

            _request = AddTokenToRequest(_request);

            return _restClient.Execute(_request).Content;
        }

        private IRestRequest AddTokenToRequest(IRestRequest request)
        {
            request.AddHeader("x-access-token", "237f37871102101c4ec439ba6c98520e");
            request.AddHeader("x-rapidapi-host", "travelpayouts-travelpayouts-flight-data-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "22f98697d5mshbe41e4f988558c4p113d77jsnb304fc8f16d7");

            return request;
        }
    }

    public interface IClientService
    {
        public string GetFlights(string origin, string destination, string currency);
    }
}

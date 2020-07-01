using FlightSharpWebSite.Util;
using Microsoft.AspNetCore.Http;

namespace FlightSharpWebSite.Services
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public virtual void SetSessionString(string key, string value)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            httpContext.Session.SetString(key, value);
        }

        public virtual void SetSessionObject(string key, object value)
        {
            _httpContextAccessor.HttpContext.Session.SetObject(key, value);
        }

        public virtual T GetSessionObject<T>(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<T>(key);
        }
    }
}

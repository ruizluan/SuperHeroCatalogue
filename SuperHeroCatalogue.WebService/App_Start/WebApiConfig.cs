using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace SuperHeroCatalogue.WebService
{
    internal static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
        }
    }
}

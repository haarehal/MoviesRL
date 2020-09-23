using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MoviesRL
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Po defaultu se koristi Pascal notacija (prvo slovo svakog polja je veliko) u JSON objektima koje API vraca
            // Omogucavanje Camel notacije koja se koristi u javascriptu u svrhu izbjegavanja "ruznog" koda:
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
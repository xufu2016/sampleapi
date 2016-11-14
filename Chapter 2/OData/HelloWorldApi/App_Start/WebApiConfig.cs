using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Microsoft.Data.Edm;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace HelloWorldApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapODataServiceRoute("modelOData", "OData", GenerateEdmModel());
        }

        private static IEdmModel GenerateEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Models.Model>("Models");

            return builder.GetEdmModel();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;
using System.Web.ModelBinding;
using HelloApi.Models;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using SimpleModelBinderProvider = System.Web.Http.ModelBinding.Binders.SimpleModelBinderProvider;

namespace HelloApi
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
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // You can globally register a custom model binder in the HttpConfiguration
            // You still need to decorate the paramters in action methods with [ModelBinder],
            // but you don't have to specify which model binder there
            //var productProvider = new SimpleModelBinderProvider(typeof(Product), new ProductModelBinder());
            //config.Services.Insert(typeof(ModelBinderProvider), 0, productProvider);

            // You can globally register value providers this way
            // The first provider that matches will return the value
            //config.Services.Add(typeof(ValueProviderFactory), new HeadersValueProviderFactory());

            // You can set up ad-hoc HttpParameterBinding rules in the
            // HttpConfiguration this way.
            //config.ParameterBindingRules.Add(p =>
            //{
            //    if (p.ParameterType == typeof(Product) &&
            //        p.ActionDescriptor.SupportedHttpMethods.Contains(HttpMethod.Get))
            //    {
            //        return new ProductParameterBinder(p);
            //    }
            //    // always return null if the binding is not applicable
            //    return null;
            //});
        }
    }
}

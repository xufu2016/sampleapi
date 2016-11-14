using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;

namespace HelloApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // *************************************
            // HttpContext and OWIN Context examples
            // *************************************

            // Application state available to all users and threads
            HttpApplicationStateBase applicationState = HttpContext.Application;
            // Session state
            HttpSessionStateBase sessionState = HttpContext.Session;
            // Server info and utility functions (like encode/decode and MapPath)
            HttpServerUtilityBase serverInfo = HttpContext.Server;
            // The HTTP Request in a strongly-typed object
            HttpRequestBase request = HttpContext.Request;
            // The HTTP Response in a strongly-typed object
            HttpResponseBase response = HttpContext.Response;


            // HttpContext.User is an IPrincipal
            IPrincipal user = HttpContext.User;
            // OWIN context User is typed as a ClaimsIdentity
            ClaimsPrincipal claimsPrincipal = HttpContext.GetOwinContext().Authentication.User;
            // ...which exposes claims
            IEnumerable<Claim> claims = claimsPrincipal.Claims;


            // The OWIN context is available two ways:
            IOwinContext owinContext = HttpContext.GetOwinContext();
            IOwinContext owinContextAgain = Request.GetOwinContext();

            // Loop through the OWIN context and print it alphabetically
            ViewBag.OwinText = "";
            foreach(var k in owinContext.Environment.Keys.OrderBy(k => k))
            {
                ViewBag.OwinText += $"{k}: {owinContext.Environment[k]}<br/>";
            }

            // How to reconstruct the URI from the OWIN Context
            var uri =
                  (string)owinContext.Environment["owin.RequestScheme"] + "://" +
                  ((IDictionary<string, string[]>)owinContext.Environment["owin.RequestHeaders"])["host"].First() +
                  (string)owinContext.Environment["owin.RequestPathBase"] +
                  (string)owinContext.Environment["owin.RequestPath"];
            if ((string)owinContext.Environment["owin.RequestQueryString"] != "")
                uri += "?" + (string)owinContext.Environment["owin.RequestQueryString"];

            // Print the OWIN Context on the view
            ViewBag.OwinText = uri + "<br/><br/>" + ViewBag.OwinText;



            // ********************
            // HttpRequest examples
            // ********************

            // Params includes the query string, form data, server variables, and cookies
            NameValueCollection requestParams = Request.Params;
            // just the form data
            NameValueCollection requestFormData = Request.Form;
            // just the headers data
            NameValueCollection requestHeaders = Request.Headers;
            // just the cookies
            HttpCookieCollection requestCookies = Request.Cookies;
            // just the posted files
            HttpFileCollectionBase requestFilesData = Request.Files;
            // just the query string data
            NameValueCollection requestQueryStringData = Request.QueryString;


            // *********************
            // HttpResponse examples
            // *********************


            // Set the HTTP status code (200,404,500,etc...)
            int responseStatusCode = Response.StatusCode;
            // Customize the HTTP status description (great for error codes)
            string responseDescription = Response.StatusDescription;
            // Customize the caching policy for the returned content
            HttpCachePolicyBase responseCachePolicy = Response.Cache;
            // Set the content type of the response
            string responseContentType = Response.ContentType;
            // Add/Remove/Customize HTTP headers on the response
            NameValueCollection responseHeaders = Response.Headers;
            // Add/Remove/Customize cookies on the response to the browser
            HttpCookieCollection responseCookies = Response.Cookies;

            // And dozens of methods used to send content to the browser:

            //Response.AddHeader(...)
            //Response.AppendCookie(...)
            //Response.Write(...)
            //Response.Redirect(...)
            //Response.TransmitFile(...)


            return View();
        }
    }
}

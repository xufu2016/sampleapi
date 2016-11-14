using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using HelloApi.Models;

namespace HelloApi.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/VehicleLocator/{year:int}/{make:alpha}/{model}")]
        public IHttpActionResult UriInput(int year, string make, string model, bool preOwned = true)
        {
            Debugger.Break();

            return Ok();
        }

        public class Vehicle
        {
            public int Year { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public bool PreOwned { get; set; }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("api/VehicleLocator/")]
        public IHttpActionResult BodyInput(Vehicle vehicle)
        {
            Debugger.Break();

            return Ok();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/Values/BindSimpleParameterFromBody")]
        public IHttpActionResult BindSimpleParameterFromBody([FromBody] string value)
        {
            Debugger.Break();

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Values/ReceiveProductFromUri")]
        public IHttpActionResult BindProductFromUri([FromUri]Product product)
        {
            Debugger.Break();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Values/ReceiveFileInput")]
        public async Task<IHttpActionResult> ReceiveFileInput()
        {
            // Verify that this is an HTML Form file upload request
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            }

            // Create a stream provider for setting up output streams
            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(Path.GetTempPath());

            // Read the MIME multipart asynchronously content using the stream provider we just created.
            var provider = await Request.Content.ReadAsMultipartAsync(streamProvider);

            // Here is how to get metadata about the uploaded file
            var fileData = provider.FileData.First();
            Console.WriteLine(fileData.Headers.ContentType);
            Console.WriteLine(fileData.Headers.ContentDisposition);

            // Here is how to access the uploaded file
            var fileText = File.ReadAllText(fileData.LocalFileName);

            Debugger.Break();

            return Ok();
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("api/Values/ReceiveSimpleProductFromUri")]
        public IHttpActionResult BindSimpleProductFromUri(SimpleProduct product)
        {
            Debugger.Break();

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Values/ModelBindProductFromUri")]
        public IHttpActionResult ModelBindProductFromUri(
            [ModelBinder(typeof(ProductModelBinder))]Product product)
        {
            Debugger.Break();

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Values/BindFavoriteColor")]
        public IHttpActionResult BindValueFromHeader(
            [ValueProvider(typeof(HeadersValueProviderFactory))] string favoriteColor)
        {
            Debugger.Break();

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Values/CustomHttpParameterBinding")]
        public IHttpActionResult CustomHttpParameterBinding(
            [ProductBinding] Product product)
        {
            Debugger.Break();

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Values/CustomModelAttributeBinding")]
        public IHttpActionResult CustomModelAttributeBinding(
            [ProductModelBinder] Product product)
        {
            Debugger.Break();

            return Ok();
        }
    }
}

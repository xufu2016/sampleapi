using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HelloWorldApi.Controllers
{
    public class HelloWorldController : ApiController
    {
        #region Action Results       

        //Actions can return an IHttpActionResult giving you full control of what to return.
        [HttpGet]
        public IHttpActionResult FileResult()
        {
            return new FileResult(@"~\TextFile1.txt");
        }

        //Actions can return a resource
        [HttpGet]
        public Customer Customer()
        {
            var customer = new Customer();
            return customer;
        }

        #endregion
    }

    #region Customer

    public class Customer
    {
        public int Id { get; set; }
    }

    #endregion

    #region FileResult

    public class FileResult : IHttpActionResult
    {
        private readonly string _path;
        private readonly string _contentType;

        public FileResult(string path, string contentType = null)
        {
            _path = path;
            _contentType = contentType;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            string mappedPath = System.Web.HttpContext.Current.Request.MapPath(_path);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(File.OpenRead(mappedPath))
            };

            var contentType = _contentType ?? MimeMapping.GetMimeMapping(Path.GetExtension(mappedPath));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

            return Task.FromResult(response);
        }
    }

    #endregion

}

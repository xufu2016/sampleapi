using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace HelloWorldApi.Controllers
{
    public class HelloWorldController : ApiController
    {
        #region HttpStatusCodes

        //Actions returning void have a default HttpStatusCode of 204 (No Content)
        [HttpGet]
        public void Void()
        {

        }

        //Actions returning objects have a default HttpStatusCode 200 (OK)
        [HttpGet]
        public Customer Customer()
        {
            var customer = new Customer();
            return customer;
        }

        //Actions returning HttpResponseMessage give you control over the response
        [HttpGet]
        public HttpResponseMessage HttpResponseMessage()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, new Customer());
            return response;
        }

        //There are many shortcut IHttpActionResult methods in System.Web.Http.Results, but not all HttpStatusCodes are covered
        [HttpGet]
        public IHttpActionResult BuiltInIHttpActionResult()
        {
            var response = Ok(new Customer());
            return response;
            //return NotFound();
            //etc...
        }

        //Actions returning IHttpActionResult can defer responsibility of constructing the response to another class
        [HttpGet]
        public IHttpActionResult CustomIHttpActionResult()
        {
            var response = new CustomHttpActionResult<Customer>(Request, new Customer());
            return response;
        }

        //You are free to cast any int to an HttpStatusCode
        [HttpGet]
        public HttpResponseMessage CustomHttpStatusCode()
        {
            var response = Request.CreateResponse((HttpStatusCode)475, new Customer());
            return response;
        }

        //Actions that throw exceptions have a default HttpStatusCode of 500 (Internal Server Error)
        [HttpGet]
        public void NotImplementedException()
        {
            throw new NotImplementedException();
        }

        //Actions that throw the HttpResponseException give you control over the HttpStatusCode of the response
        [HttpGet]
        public void HttpResponseException()
        {
            throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
        }

        #endregion

    }

    #region Customer

    public class Customer
    {
        public int Id { get; set; }
    }

    #endregion

    #region CustomHttpActionResult

    public class CustomHttpActionResult<T> : IHttpActionResult
    {
        HttpRequestMessage _request;
        T _content;

        public CustomHttpActionResult(HttpRequestMessage request, T content)
        {
            _request = request;
            _content = content;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(HttpStatusCode.OK, _content);
            return Task.FromResult(response);
        }
    }

    #endregion
}

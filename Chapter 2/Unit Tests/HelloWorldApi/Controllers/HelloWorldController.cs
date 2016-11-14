using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorldApi.Controllers
{
    public class HelloWorldController : ApiController
    {
        [HttpGet]
        public void Void(int id)
        {
            if (id == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public HttpResponseMessage HttpResponseMessage(int id)
        {
            if (id == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            return Request.CreateResponse(HttpStatusCode.OK, new Resource());
        }

        [HttpGet]
        public IHttpActionResult IHttpActionResult(int id)
        {
            if (id == 0)
                return BadRequest();

            return Ok(new Resource());
        }

        [HttpGet]
        public Resource Resource(int id)
        {
            if (id == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            return new Resource();
        }
    }

    public class Resource
    {
        
    }
}

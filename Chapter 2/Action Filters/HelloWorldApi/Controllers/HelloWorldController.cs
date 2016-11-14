using System.Web.Http;

namespace HelloWorldApi.Controllers
{
    public class HelloWorldController : ApiController
    {
        [HttpGet]
        public IHttpActionResult HttpGet()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }

        [HttpPost]
        public IHttpActionResult HttpPost()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }

        [AcceptVerbs("PUT", "PATCH")]
        public IHttpActionResult AcceptVerbs()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }

        [ActionName("Name")]
        [HttpGet]
        public IHttpActionResult ActionName()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }

        [NonAction]
        [HttpGet]
        public void NonAction()
        {
            
        }

        [HttpGet]
        [Authorize]
        public void Authorize()
        {

        }

        [Route("api/v2/HelloWorld/Route")]
        [HttpGet]
        public IHttpActionResult Route()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        } 
    }
}

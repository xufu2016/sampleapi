using System.Web.Http;

namespace HelloWorldApi.Controllers
{
    [RoutePrefix("api/v2/routes")]
    public class RouteController : ApiController
    {
        [Route("literal")]
        [HttpGet]
        public IHttpActionResult Literal()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }
    }
}
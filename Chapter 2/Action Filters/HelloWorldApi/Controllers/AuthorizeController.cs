using System.Web.Http;

namespace HelloWorldApi.Controllers
{
    [Authorize]
    public class AuthorizeController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult AllowAnonymous()
        {
            return Ok("Success");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace HelloWorldApi.Controllers
{
    public class HelloWorldController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/Authorize")]
        public IHttpActionResult Authorize()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }

        [Authorize(Users = "test@test.com")]
        [HttpGet]
        [Route("api/AuthorizeWithUsers")]
        public IHttpActionResult AuthorizeWithUsers()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("api/AuthorizeWithRoles")]
        public IHttpActionResult AuthorizeWithRoles()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }
    }
}

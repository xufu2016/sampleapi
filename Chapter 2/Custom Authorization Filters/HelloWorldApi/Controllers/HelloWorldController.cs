using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HelloWorldApi.Controllers
{
    public class HelloWorldController : ApiController
    {
        [HttpGet]
        [Route("api/CustomAuthorizeAttribute")]
        [CustomAuthorize]
        public IHttpActionResult CustomAuthorizeAttribute()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }

        [HttpGet]
        [CustomAuthorizationFilter]
        [Route("api/CustomAuthorizationFilterAttribute")]
        public IHttpActionResult CustomAuthorizationFilterAttribute()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }

        [HttpGet]
        [Route("api/CustomInterfacedAuthorizationFilterAttribute")]
        [CustomInterfacedAuthorizationFilter]
        public IHttpActionResult CustomInterfacedAuthorizationFilterAttribute()
        {
            return Ok(ActionContext.ActionDescriptor.ActionName);
        }
    }

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //Normally do this
            //if (!base.IsAuthorized(actionContext))
            //    return false;

            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("authorized", out values))
            {
                bool result = false;

                if (values.Any(x => bool.TryParse(x, out result)) && result)
                    return true;
            }

            return false;
        }
    }

    public class CustomAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("authorized", out values))
            {
                bool result = false;

                if (values.Any(x => bool.TryParse(x, out result)) && result)
                    return;
            }

            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization has been denied for this request.");
        }
    }

    public class CustomInterfacedAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("authorized", out values))
            {
                bool result = false;

                if (values.Any(x => bool.TryParse(x, out result)) && result)
                    return continuation();
            }

            return Task.FromResult(actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization has been denied for this request."));
        }
    }
}

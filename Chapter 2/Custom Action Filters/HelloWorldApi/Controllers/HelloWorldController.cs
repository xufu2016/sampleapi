using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using HelloWorldApi.Filters;

namespace HelloWorldApi.Controllers
{
    public class HelloWorldController : ApiController
    {
        #region Custom Action Filters

        [HttpGet]
        [CustomActionFilter]
        public void CustomActionFilter()
        {
            Trace.WriteLine(string.Format("CustomActionFilterAttribute - ActionName:{0} {1}", ActionContext.ActionDescriptor.ActionName, "Body of Action Method"));
        }

        [HttpGet]
        [CustomExecutingReplacedResponseActionFilter]
        public IHttpActionResult CustomExecutingReplacedResponseActionFilter()
        {
            return Ok(string.Format("CustomExecutingReplacedResponseActionFilter - ActionName:{0}", ActionContext.ActionDescriptor.ActionName));
        }

        [HttpGet]
        [CustomExecutedReplacedResponseActionFilter]
        public IHttpActionResult CustomExecutedReplacedResponseActionFilter()
        {
            return Ok(string.Format("CustomExecutedReplacedResponseActionFilter - ActionName:{0}", ActionContext.ActionDescriptor.ActionName));
        }

        [HttpGet]
        [CustomSynchronousActionFilter(order: 1)]
        [CustomSynchronousActionFilter(order: 2)]
        [CustomAsynchronousActionFilter(order: 1)]
        public void OrderedCustomActionFilter()
        {

        }

        #endregion

    }

    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Trace.WriteLine(string.Format("CustomActionFilterAttribute - ActionName:{0} {1}", actionContext.ActionDescriptor.ActionName, "OnActionExecuting"));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Trace.WriteLine(string.Format("CustomActionFilterAttribute - ActionName:{0} {1}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, "OnActionExecuted"));
        }
    }

    public class CustomExecutingReplacedResponseActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, "Response Replaced OnActionExecuting.");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, "Response Replaced OnActionExecuted.");
        }
    }

    public class CustomExecutedReplacedResponseActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, "Response Replaced.");
        }
    }

    public class BaseActionFilterAttribute : ActionFilterAttribute, IOrderedFilter
    {
        protected readonly int _order;

        public BaseActionFilterAttribute(int order) { _order = order; }

        public int Order
        {
            get
            {
                return _order;
            }
        }
    }

    public class CustomSynchronousActionFilterAttribute : BaseActionFilterAttribute
    {
        public CustomSynchronousActionFilterAttribute(int order) : base(order) { }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Trace.WriteLine(string.Format("CustomSynchronousActionFilterAttribute - ActionName:{0} Order:{1} {2}", actionContext.ActionDescriptor.ActionName, _order, "OnActionExecuting"));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Trace.WriteLine(string.Format("CustomSynchronousActionFilterAttribute - ActionName:{0} Order:{1} {2}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, _order, "OnActionExecuted"));
        }

        public override bool AllowMultiple
        {
            get
            {
                return true;
            }
        }
    }

    public class CustomAsynchronousActionFilterAttribute : BaseActionFilterAttribute
    {
        public CustomAsynchronousActionFilterAttribute(int order) : base(order) { }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
                Trace.WriteLine(string.Format("CustomAsynchronousActionFilterAttribute - ActionName:{0} Order:{1} {2}", actionContext.ActionDescriptor.ActionName, _order, "OnActionExecutingAsync"))
            );
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
                Trace.WriteLine(string.Format("CustomAsynchronousActionFilterAttribute - ActionName:{0} Order:{1} {2}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, _order, "OnActionExecutedAsync"))
            );
        }
    }
}

using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HelloApi.Middleware
{
    public class LoggingMiddleware : OwinMiddleware
    {
        public LoggingMiddleware(OwinMiddleware next) : base(next)
        {

        }

        public override async Task Invoke(IOwinContext context)
        {
            Debug.WriteLine($"Logging remote IP address {context.Request.RemoteIpAddress} on remote port {context.Request.RemotePort}.");
            await this.Next.Invoke(context);
            Debug.WriteLine($"Request from remote IP address {context.Request.RemoteIpAddress} completed.");
        }
    }

    public static class LoggingMiddlewareExtensions
    {
        public static IAppBuilder UseLogging(this IAppBuilder app)
        {
            app.Use<LoggingMiddleware>();
            app.UseStageMarker(PipelineStage.PreHandlerExecute);
            return app;
        }
    }
}
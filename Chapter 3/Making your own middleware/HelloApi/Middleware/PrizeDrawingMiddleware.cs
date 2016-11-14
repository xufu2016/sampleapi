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
    public class PrizeDrawingMiddleware : OwinMiddleware
    {
        private readonly int WinningNumber;

        public PrizeDrawingMiddleware(OwinMiddleware next) : base(next)
        {
            WinningNumber = new Random().Next(1, 3);
        }

        public override async Task Invoke(IOwinContext context)
        {
            var thisNumber = new Random().Next(1, 3);
            if (thisNumber == GetWinningNumber())
            {
                context.Set("PrizeDrawingWinner", context.Request.User.Identity.Name);
                Debug.WriteLine("Congratulations, you won the prize drawing!");
            }
            else
            {
                Debug.WriteLine("Sorry, you didn't win the prize drawing.");
            }
            await this.Next.Invoke(context);
        }

        protected int GetWinningNumber()
        {
            return WinningNumber;
        }
    }

    public static class PrizeDrawingMiddlewareExtensions
    {
        public static IAppBuilder UsePrizeDrawing(this IAppBuilder app)
        {
            app.Use<PrizeDrawingMiddleware>();
            app.UseStageMarker(PipelineStage.PreHandlerExecute);
            return app;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using HelloApi.Middleware;

[assembly: OwinStartup(typeof(HelloApi.Startup))]

namespace HelloApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseLogging();
            app.UsePrizeDrawing();
        }
    }
}

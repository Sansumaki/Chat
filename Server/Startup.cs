using Microsoft.Owin;
using Owin;
using Servr;

[assembly: OwinStartup(typeof(Startup))]

namespace Servr
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
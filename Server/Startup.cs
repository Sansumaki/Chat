using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(Servr.Startup))]
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
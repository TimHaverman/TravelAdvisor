using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelAdvisor.Startup))]
namespace TravelAdvisor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

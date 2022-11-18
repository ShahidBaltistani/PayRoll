using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VUPayRoll.WebApp.Startup))]
namespace VUPayRoll.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

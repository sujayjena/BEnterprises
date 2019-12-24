using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BE.Web.Startup))]
namespace BE.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

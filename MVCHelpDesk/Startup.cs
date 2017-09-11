using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCHelpDesk.Startup))]
namespace MVCHelpDesk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

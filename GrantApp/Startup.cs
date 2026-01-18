using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GrantApp.Startup))]
namespace GrantApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

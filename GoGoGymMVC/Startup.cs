using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoGoGymMVC.Startup))]
namespace GoGoGymMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

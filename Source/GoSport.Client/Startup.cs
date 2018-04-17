using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoSport.Client.Startup))]
namespace GoSport.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

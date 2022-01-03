using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Training_System.Startup))]
namespace Training_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

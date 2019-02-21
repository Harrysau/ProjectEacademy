using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectEacademy.Startup))]
namespace ProjectEacademy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

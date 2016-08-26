using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSEProject.Startup))]
namespace SSEProject
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

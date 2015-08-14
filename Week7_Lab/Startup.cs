using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Week7_Lab.Startup))]
namespace Week7_Lab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

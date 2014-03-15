using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SafetyAdvisor.Startup))]
namespace SafetyAdvisor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

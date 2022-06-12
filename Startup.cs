using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracticeActivityTraking.Startup))]
namespace PracticeActivityTraking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Telephony_selling.Startup))]
namespace Telephony_selling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

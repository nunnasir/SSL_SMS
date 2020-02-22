using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSL_SMS.Startup))]
namespace SSL_SMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FreerideMTB_Store.Startup))]
namespace FreerideMTB_Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

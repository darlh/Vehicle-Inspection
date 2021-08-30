using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VehicleInspection.Startup))]
namespace VehicleInspection
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaligulasHotel.Startup))]
namespace CaligulasHotel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

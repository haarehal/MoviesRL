using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoviesRL.Startup))]
namespace MoviesRL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

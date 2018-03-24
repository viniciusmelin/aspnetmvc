using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(App.Game.Startup))]
namespace App.Game
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserManagmentSystem.Startup))]
namespace UserManagmentSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

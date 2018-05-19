using BlogWeb.Infra;
using BlogWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(BlogWeb.App_Start.Startup))]

namespace BlogWeb.App_Start
{
    public class Startup
    {

        public void Configuration(IAppBuilder builder)
        {
            var options = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Autenticacao/Login")
            };
            builder.UseCookieAuthentication(options);

            builder.CreatePerOwinContext<BlogContext>(() => new BlogContext());
            builder.CreatePerOwinContext<IUserStore<Usuario>>((opt, owinContext) =>
            {
                var context = owinContext.Get<BlogContext>();
                return new UserStore<Usuario>(context);
            });
            builder.CreatePerOwinContext<UserManager<Usuario>>((opt, owinContext) =>
            {
                var store = owinContext.Get<IUserStore<Usuario>>();
                return new UserManager<Usuario>(store);
            });
        }
    }
}
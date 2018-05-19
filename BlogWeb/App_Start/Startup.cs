using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        }
    }
}
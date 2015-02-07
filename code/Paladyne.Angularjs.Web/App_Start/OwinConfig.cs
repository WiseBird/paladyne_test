using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

using Owin;

using Paladyne.Angularjs.Web.App_Start;

[assembly: OwinStartup(typeof(OwinConfig))]

namespace Paladyne.Angularjs.Web.App_Start
{
    public class OwinConfig
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/home/index"),
            });
        }
    }
}
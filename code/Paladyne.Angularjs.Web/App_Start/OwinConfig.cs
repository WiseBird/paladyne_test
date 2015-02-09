using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

using Owin;

using Paladyne.Angularjs.Web.App_Start;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(OwinConfig))]

namespace Paladyne.Angularjs.Web.App_Start
{
    public class OwinConfig
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder builder)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            builder.UseOAuthBearerAuthentication(OAuthBearerOptions);

            builder.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
        }
    }
}
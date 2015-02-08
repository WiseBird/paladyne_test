using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Ninject;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Mvc;

namespace Paladyne.Angularjs.Web.Controllers
{
    public class AccountController : Controller
    {
        [Inject]
        public IUserService UserService { get; set; }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = UserService.GetByNameAndPassword(username, password);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            var identity = new ClaimsIdentity(OwinConfig.OAuthBearerOptions.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, username));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var currentUtc = new SystemClock().UtcNow;
            AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));

            string token = OwinConfig.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
            return Content(token);
        }
    }
}

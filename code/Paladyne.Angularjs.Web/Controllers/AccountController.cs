using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Ninject;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL.Entities;
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

        public virtual IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = UserService.GetByNameAndPassword(username, password);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            SetAuthCookie(user);
            var token = GetAuthToken(user);
            return Json(new { token, userId = user.Id });
        }

        private static string GetAuthToken(User user)
        {
            var identity = new ClaimsIdentity(OwinConfig.OAuthBearerOptions.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var currentUtc = new SystemClock().UtcNow;
            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));

            string token = OwinConfig.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
            return token;
        }

        private void SetAuthCookie(User user)
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            this.AuthenticationManager.SignOut();
            this.AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true, }, identity);
        }
    }
}

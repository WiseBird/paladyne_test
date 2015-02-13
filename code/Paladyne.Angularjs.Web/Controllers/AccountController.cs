using System.Reflection;
using System.ServiceModel.Security;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Ninject;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.BL.Models;
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

using Paladyne.Angularjs.Web.Infrastructure;
using Paladyne.Angularjs.Web.Models;
using Paladyne.Angularjs.Web.Models.Account;

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
            var user = UserService.GetByNameAndPasswordEx(username, password, new UserInclude().UserModules());
            if (user == null)
            {
                return new ServiceErrorsResult("Invalid login or password");
            }

            SetAuthCookie(user);

            return AuthOkResult(user);
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
        private ActionResult AuthOkResult(User user)
        {
            var token = GetAuthToken(user);

            return
                this.Json(new
                {
                    token,
                    userId = user.Id,
                    userName = user.UserName,
                    modules =
                        user.UserModules.Select(
                            x => new { id = x.ModuleId, name = x.ModuleName, permission = x.Permission.ToString() })
                });
        }

        [HttpPost]
        public ActionResult Register(RegisterUser model)
        {
            var user = new CreateUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Password = model.Password,
            };

            user.Modules.Add(new CreateUser.UserModule()
            {
                Id = Modules.welcome.ToString(),
                Name = Modules.welcome.ToString(),
                Permission = Permissions.See
            });
            user.Modules.Add(new CreateUser.UserModule()
            {
                Id = Modules.users.ToString(),
                Name = Modules.users.ToString(),
                Permission = Permissions.Prohibit
            });
            user.Modules.Add(new CreateUser.UserModule()
            {
                Id = Modules.userModules.ToString(),
                Name = Modules.userModules.ToString(),
                Permission = Permissions.Edit
            });

            var errors = new List<string>();
            UserService.Create(user, new ValidationErrors(errors));

            if (errors.Count != 0)
            {
                return new ServiceErrorsResult(errors);
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Token()
        {
            var user = UserService.GetByNameEx(User.Identity.Name, new UserInclude().UserModules());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return AuthOkResult(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            this.AuthenticationManager.SignOut();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}

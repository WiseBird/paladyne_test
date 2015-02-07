using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using Ninject;

using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL.Entities;
using Paladyne.Angularjs.Web.Infrastructure;

namespace Paladyne.Angularjs.Web.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public UserManager<User> UserManager { get; set; }
        
        public virtual IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var user = UserService.GetByNameAndPassword(Configuration.Instance.AdminUserName, Configuration.Instance.AdminPassword);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = true,
            }, identity);

            return RedirectToAction("index");
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("index");
        }

        [Authorize]
        public ActionResult Action()
        {
            return Json("Access");
        }
	}
}
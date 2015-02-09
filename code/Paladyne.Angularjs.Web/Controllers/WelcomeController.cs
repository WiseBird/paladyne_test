using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Ninject;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.Web.Infrastructure;
using Paladyne.Angularjs.Web.Models;

namespace Paladyne.Angularjs.Web.Controllers
{
    public class WelcomeController : ApiController
    {
        [Inject]
        public IUserService UserService { get; set; }

        [ModuleAuthorize(Modules.welcome)]
        public IHttpActionResult Get()
        {
            var user = UserService.GetByName(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { userName = user.UserName, lastLogin = user.LastLogin });
        }
    }
}

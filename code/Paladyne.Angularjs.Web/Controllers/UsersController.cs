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
    public class UsersController : ApiController
    {
        [Inject]
        public IUserService UserService { get; set; }

        [ModuleAuthorize(Modules.users)]
        public IHttpActionResult Get()
        {
            var users = UserService.GetAll(new UserInclude());
            return Ok(users);
        }

        [ModuleAuthorize(Modules.users)]
        public IHttpActionResult Get(int id)
        {
            var users = UserService.GetAll(new UserInclude());
            return Ok(users);
        }
    }
}

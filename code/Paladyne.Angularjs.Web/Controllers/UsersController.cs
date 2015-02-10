using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;

using Ninject;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL.Entities;
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
            var users = UserService.GetAll(new UserInclude().UserModules());
            return Ok(users.Select(x => new
                                            {
                                                firstName = x.FirstName,
                                                lastName = x.LastName,
                                                modules = x.UserModules.Where(y => y.Permission != Permissions.Prohibit)
                                                    .Select(y => new { id = y.ModuleId, name = y.ModuleName, permission = y.Permission.ToString()})
                                            }));
        }

        [ModuleAuthorize(Modules.users)]
        public IHttpActionResult Get(int id)
        {
            var users = UserService.GetAll(new UserInclude());
            return Ok(users);
        }
    }
}

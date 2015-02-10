using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Ninject;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL.Entities;
using Paladyne.Angularjs.Web.Infrastructure;
using Paladyne.Angularjs.Web.Models;

namespace Paladyne.Angularjs.Web.Controllers
{
    public class UserModulesController : ApiController
    {
        [Inject]
        public IUserService UserService { get; set; }

        [ModuleAuthorize(Modules.userModules)]
        public IHttpActionResult Get()
        {
            var user = UserService.GetByNameEx(User.Identity.Name, new UserInclude().UserModules(true));
            return Ok(user.UserModules.Select(x => new
            {
                id = x.ModuleId,
                name = x.ModuleName,
                granter = x.Granter == null ? "System" : x.Granter.UserName
            }));
        }

        // POST api/module
        public void Post([FromBody]string value)
        {
        }

        // PUT api/module/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/module/5
        public void Delete(int id)
        {
        }
    }
}

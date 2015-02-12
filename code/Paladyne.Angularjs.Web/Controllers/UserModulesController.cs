using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

using Ninject;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL.Entities;
using Paladyne.Angularjs.Web.Infrastructure;
using Paladyne.Angularjs.Web.Models;
using Paladyne.Angularjs.Web.Models.UserModules;

namespace Paladyne.Angularjs.Web.Controllers
{
    public class UserModulesController : ApiController
    {
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IUserModuleService UserModuleService { get; set; }

        [ModuleAuthorize(Modules.userModules)]
        public IHttpActionResult Get()
        {
            var user = UserService.GetByNameEx(User.Identity.Name, new UserInclude().UserModules(true));
            return Ok(user.UserModules.Where(x => x.Permission != Permissions.Prohibit).Select(x => new
            {
                id = x.ModuleId,
                name = x.ModuleName,
                granter = x.Granter == null ? "System" : x.Granter.UserName
            }));
        }

        [ModuleAuthorize(Modules.userModules, Permissions.Edit)]
        public IHttpActionResult Put(string id, [FromBody]UpdateUserModule model)
        {
            var user = UserService.GetByName(User.Identity.Name);
            if (user == null)
            {
                return this.StatusCode(HttpStatusCode.Unauthorized);
            }

            var blModel = new BL.Models.UpdateUserModule()
                              {
                                  UserId = user.Id,
                                  ModuleId = id,
                                  ModuleName = model.Name
                              };

            var errors = new List<string>();
            UserModuleService.Update(blModel, new ValidationErrors(errors));
            if (errors.Count != 0)
            {
                return new ServiceErrorsResult(errors);
            }
            else
            {
                return this.StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}

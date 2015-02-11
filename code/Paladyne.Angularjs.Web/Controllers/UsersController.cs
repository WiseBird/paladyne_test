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
using Paladyne.Angularjs.Web.Models.Users;

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
                                                id = x.Id,
                                                firstName = x.FirstName,
                                                lastName = x.LastName,
                                                modules = x.UserModules
                                                    .Select(y => new { id = y.ModuleId, name = y.ModuleName, permission = y.Permission.ToString()})
                                            }));
        }

        [ModuleAuthorize(Modules.users, Permissions.Edit)]
        public IHttpActionResult Put(string id, [FromBody] UpdateUserData model)
        {
            var user = UserService.GetByName(User.Identity.Name);
            if (user == null)
            {
                return this.StatusCode(HttpStatusCode.Unauthorized);
            }

            var blModel = new BL.Models.UpdateUserData()
            {
                UserId = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Modules = model.Modules.Select(x => new BL.Models.UpdateUserData.UserModule()
                                                        {
                                                            ModuleId = x.Id,
                                                            Permission = x.Permission,
                                                            GranterId = user.Id
                                                        }).ToList()
            };

            var errors = new List<string>();
            UserService.Update(blModel, new ValidationErrors(errors));
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

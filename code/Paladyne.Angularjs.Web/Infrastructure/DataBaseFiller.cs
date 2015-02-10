using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ninject;

using Paladyne.Angularjs.BL;
using Paladyne.Angularjs.BL.Models;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL.Entities;
using Paladyne.Angularjs.Web.Models;

namespace Paladyne.Angularjs.Web.Infrastructure
{
    public interface IDBFillerConfiguration
    {
        string AdminUserName { get; }
        string AdminPassword { get; }
    }

    public class DataBaseFiller
    {
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IDBFillerConfiguration Configuration { get; set; }

        public void Fill()
        {
            EnsureAdminUser();
        }

        private void EnsureAdminUser()
        {
            if (UserService.ExistsByName(Configuration.AdminUserName))
            {
                return;
            }

            var admin = new CreateUser()
                            {
                                FirstName = "admin",
                                LastName = "admin",
                                UserName = Configuration.AdminUserName,
                                Password = Configuration.AdminPassword,
                            };

            foreach (var moduleName in Enum.GetNames(typeof(Modules)))
            {
                admin.Modules.Add(new CreateUser.UserModule()
                                      {
                                          ModuleId = moduleName,
                                          ModuleName = moduleName,
                                          Permission = Permissions.Edit
                                      });
            }

            UserService.Create(admin, new ValidationErrors(
                (p, e) =>
                    {
                        throw new Exception("Validation error during admin creation: " + e);
                    }));
        }
    }
}
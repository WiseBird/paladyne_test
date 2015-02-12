using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Paladyne.Angularjs.BL.Models;
using Paladyne.Angularjs.DAL;
using Paladyne.Angularjs.DAL.Entities;
using Paladyne.Angularjs.DAL.Tests;

namespace Paladyne.Angularjs.BL.Tests.Services
{
    [TestFixture]
    public class UserModuleServiceTests
    {
        [Test]
        public void Update_WhenModuleMissing_Throws()
        {
            using (var context = ContextHelper.Create())
            {
                var model = GetValidUpdateUserModule(context);
                model.ModuleId = "missing";

                var service = Factory.CreateUserModuleService(context);


                var ex = Assert.Throws<Exception>(
                    () => service.Update(model, null));

                StringAssert.Contains("not found", ex.Message.ToLower());
            }
        }
        [Test]
        public void Update_WhenModuleProhibited_Throws()
        {
            using (var context = ContextHelper.Create())
            {
                var userModule = context.AddUserModule(x => { x.Permission = Permissions.Prohibit; });
                var model = GetValidUpdateUserModule(context, userModule);

                var service = Factory.CreateUserModuleService(context);


                var ex = Assert.Throws<Exception>(
                    () => service.Update(model, null));

                StringAssert.Contains("access", ex.Message.ToLower());
            }
        }

        private static UpdateUserModule GetValidUpdateUserModule(IUnitOfWork context, UserModule userModule = null)
        {
            userModule = userModule ?? context.EnsureUserModule();

            return new UpdateUserModule()
            {
                ModuleId = userModule.ModuleId,
                UserId = userModule.UserId,
                ModuleName = userModule.ModuleName
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;

using Microsoft.AspNet.Identity;

using NUnit.Framework;

using Paladyne.Angularjs.BL.Models;
using Paladyne.Angularjs.DAL;
using Paladyne.Angularjs.DAL.Entities;
using Paladyne.Angularjs.DAL.Tests;

namespace Paladyne.Angularjs.BL.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void Create_WhenUserNameIsTaken_ValidationError()
        {
            using (var context = ContextHelper.Create())
            {
                var user = context.AddUser();

                var model = GetValidCreateUser();
                model.UserName = user.UserName;

                var service = Factory.CreateUserService(context);
                var mock = A.Fake<IValidationErrors>();


                Helper.Suppress(() => service.Create(model, mock));


                A.CallTo(() => mock.Add(A<string>._, A<string>.That.Contains("taken"))).MustHaveHappened();
            }
        }
        [Test]
        public void Create_Always_CallUserManagerCreate()
        {
            using (var context = ContextHelper.Create())
            {
                var model = GetValidCreateUser();

                var mock = A.Fake<UserManager<User>>();
                var service = Factory.CreateUserService(context, mock);


                Helper.Suppress(() => service.Create(model, null));


                A.CallTo(() => mock.CreateAsync(A<User>._, model.Password)).MustHaveHappened();
            }
        }

        [Test]
        public void Update_WhenUserMissing_Throws()
        {
            using (var context = ContextHelper.Create())
            {
                var model = GetValidUpdateUserData(context);
                model.UserId = "missing";

                var service = Factory.CreateUserService(context);


                var ex = Assert.Throws<Exception>(
                    () => service.Update(model, null));

                StringAssert.Contains("not found", ex.Message.ToLower());
            }
        }

        [Test]
        public void Update_WhenNonexistentModule_IgnoresIt()
        {
            using (var context = ContextHelper.Create())
            {
                var model = GetValidUpdateUserData(context);
                model.Modules.Add(GetValidUserModule());

                var service = Factory.CreateUserService(context);


                Helper.Suppress(() => service.Update(model, null));
                var userModuleWasAdded = context.UserModules.Any();


                Assert.False(userModuleWasAdded);
            }
        }

        private static CreateUser GetValidCreateUser()
        {
            return new CreateUser()
            {
                UserName = "QWERTY",
                Password = GetValidPassword(),
                FirstName = "FirstName",
                LastName = "LastName",
            };
        }
        private static string GetValidPassword()
        {
            return "123456";
        }

        private static UpdateUserData GetValidUpdateUserData(IUnitOfWork context)
        {
            return new UpdateUserData()
            {
                UserId = context.EnsureUser().Id,
                FirstName = "FirstName",
                LastName = "LastName"
            };
        }

        public static UpdateUserData.UserModule GetValidUserModule()
        {
            return new UpdateUserData.UserModule()
                                      {
                                          ModuleId = "nonexistent",
                                          Permission = Permissions.Prohibit
                                      };
        }
    }
}

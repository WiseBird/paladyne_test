using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;

using NUnit.Framework;

using Paladyne.Angularjs.BL;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL.Entities;
using Paladyne.Angularjs.DAL.Tests;
using Paladyne.Angularjs.Web.Controllers;
using Paladyne.Angularjs.Web.Infrastructure;
using Paladyne.Angularjs.Web.Models.UserModules;

namespace Paladyne.Angularjs.Web.Tests.Controllers
{
    [TestFixture]
    public class UserModulesControllerTests
    {
        [Test]
        public void Put_Always_CallsUserModuleServiceUpdate()
        {
            var userService = A.Fake<IUserService>();
            A.CallTo(() => userService.GetByName(A<string>._)).Returns(new User());

            var mockUserModuleService = A.Fake<IUserModuleService>();

            var controller = CreateController(userService, mockUserModuleService);
            

            Helper.Suppress(() => controller.Put("", new UpdateUserModule()));


            A.CallTo(() => mockUserModuleService.Update(A<BL.Models.UpdateUserModule>._, A<IValidationErrors>._)).MustHaveHappened();
        }

        [Test]
        public void Put_WhenWalidationError_ReturnsServiceErrorsResult()
        {
            var userService = A.Fake<IUserService>();
            A.CallTo(() => userService.GetByName(A<string>._)).Returns(new User());

            var userModuleService = A.Fake<IUserModuleService>();
            A.CallTo(() => userModuleService.Update(A<BL.Models.UpdateUserModule>._, A<IValidationErrors>._)).Invokes(
                    (BL.Models.UpdateUserModule model, IValidationErrors errors) => errors.Add("", "error"));

            var controller = CreateController(userService, userModuleService);


            var result = controller.Put("", new UpdateUserModule());


            Assert.IsInstanceOf<ServiceErrorsResult>(result);
        }

        private static UserModulesController CreateController(IUserService userService = null, IUserModuleService userModuleService = null)
        {
            userService = userService ?? A.Fake<IUserService>();
            userModuleService = userModuleService ?? A.Fake<IUserModuleService>();

            return new UserModulesController() { UserService = userService, UserModuleService = userModuleService };
        }
    }
}

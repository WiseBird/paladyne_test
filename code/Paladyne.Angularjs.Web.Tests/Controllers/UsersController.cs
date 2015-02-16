using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;

using NUnit.Framework;

using Paladyne.Angularjs.BL;
using Paladyne.Angularjs.BL.Models;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL.Entities;
using Paladyne.Angularjs.DAL.Tests;
using Paladyne.Angularjs.Web.Controllers;
using Paladyne.Angularjs.Web.Infrastructure;

namespace Paladyne.Angularjs.Web.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        [Test]
        public void Put_Always_SetsModuleGrantrIdToCurrentUser()
        {
            const string currentUserId = "currentUserId";

            var mockUserService = A.Fake<IUserService>();
            A.CallTo(() => mockUserService.GetByName(A<string>._)).Returns(new User() { Id = currentUserId });

            var controller = CreateController(mockUserService);

            var model = new UpdateUserData();
            model.Modules.Add(new UpdateUserData.UserModule() { GranterId = "fake" });


            Helper.Suppress(() => controller.Put("", model));


            A.CallTo(() => mockUserService.Update(A<UpdateUserData>._, A<IValidationErrors>._)).WhenArgumentsMatch(
                (x) =>
                    {
                        var mdl = x.Get<UpdateUserData>(0);
                        return mdl.Modules.Any(y => y.GranterId != currentUserId);
                    });
        }

        [Test]
        public void Put_Always_CallsUserServiceUpdate()
        {
            var mockUserService = A.Fake<IUserService>();
            A.CallTo(() => mockUserService.GetByName(A<string>._)).Returns(new User());

            var controller = CreateController(mockUserService);


            Helper.Suppress(() => controller.Put("", new UpdateUserData()));


            A.CallTo(() => mockUserService.Update(A<UpdateUserData>._, A<IValidationErrors>._)).MustHaveHappened();
        }

        [Test]
        public void Put_WhenWalidationError_ReturnsServiceErrorsResult()
        {
            var mockUserService = A.Fake<IUserService>();
            A.CallTo(() => mockUserService.GetByName(A<string>._)).Returns(new User());

            A.CallTo(() => mockUserService.Update(A<UpdateUserData>._, A<IValidationErrors>._)).Invokes(
                    (UpdateUserData model, IValidationErrors errors) => errors.Add("", "error"));

            var controller = CreateController(mockUserService);


            var result = controller.Put("", new UpdateUserData());


            Assert.IsInstanceOf<ServiceErrorsResult>(result);
        }

        private UsersController CreateController(IUserService userService = null)
        {
            userService = userService ?? A.Fake<IUserService>();

            return new UsersController()
            {
                UserService = userService
            };
        }
    }
}

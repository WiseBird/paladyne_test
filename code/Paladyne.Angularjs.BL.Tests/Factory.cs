using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;

using Microsoft.AspNet.Identity;

using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL;
using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.BL.Tests
{
    public class Factory
    {
        public static UserModuleService CreateUserModuleService(
            IUnitOfWork unitOfWork = null)
        {
            unitOfWork = unitOfWork ?? A.Fake<IUnitOfWork>();

            return new UserModuleService(unitOfWork);
        }
        public static UserService CreateUserService(
            IUnitOfWork unitOfWork = null,
            UserManager<User> userManager = null)
        {
            unitOfWork = unitOfWork ?? A.Fake<IUnitOfWork>();
            userManager = userManager ?? A.Fake<UserManager<User>>();

            return new UserService(unitOfWork, userManager);
        }
    }
}

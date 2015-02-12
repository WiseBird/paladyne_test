using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.BL.Models;
using Paladyne.Angularjs.DAL;
using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.BL.Services
{
    public interface IUserModuleService
    {
        List<UserModule> GetAll(Include<UserModule> include);

        void Update(UpdateUserModule model, IValidationErrors errors);
    }

    public class UserModuleService : BaseService, IUserModuleService
    {
        public UserModuleService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public List<UserModule> GetAll(Include<UserModule> include)
        {
            return UnitOfWork.UserModules.AsNoTracking().Include(include).ToList();
        }

        public void Update(UpdateUserModule model, IValidationErrors errors)
        {
            if (!model.Validate(errors))
            {
                return;
            }

            var userModule = UnitOfWork.UserModules.FirstOrDefault(x => x.UserId == model.UserId && x.ModuleId == model.ModuleId);
            if (userModule == null)
            {
                throw new Exception("User module not found");
            }

            if (userModule.Permission == Permissions.Prohibit)
            {
                throw new Exception("Access denied");
            }

            model.MapTo(userModule);
            UnitOfWork.SaveChanges();
        }
    }
}

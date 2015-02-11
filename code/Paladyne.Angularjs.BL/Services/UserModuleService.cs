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

            var userModule = new UserModule()
                                 {
                                     UserId = model.UserId,
                                     ModuleId = model.ModuleId
                                 };
            UnitOfWork.UserModules.Attach(userModule);
            model.MapTo(userModule);
            UnitOfWork.SaveChanges();
        }
    }
}

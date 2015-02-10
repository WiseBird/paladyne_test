using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.DAL;
using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.BL.Services
{
    public interface IUserModuleService
    {
        List<UserModule> GetAll(Include<UserModule> include);
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
    }
}

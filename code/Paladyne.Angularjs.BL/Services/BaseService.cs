using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Paladyne.Angularjs.DAL;

namespace Paladyne.Angularjs.BL.Services
{
    public abstract class BaseService
    {
        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; set; }
    }
}

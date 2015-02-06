using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.DAL
{
    public interface IUnitOfWork
    {
        IDbSet<User> Users { get; set; }
        IDbSet<UserModule> UserModules { get; set; }

        int SaveChanges();
    }
}

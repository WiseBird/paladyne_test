using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.BL.Includes
{
    public class UserModuleInclude : Include<UserModule>
    {
        private bool user;
        private bool granter;

        public UserModuleInclude User()
        {
            user = true;
            return this;
        }
        public UserModuleInclude Granter()
        {
            granter = true;
            return this;
        }

        public override IQueryable<UserModule> Execute(IQueryable<UserModule> query)
        {
            if (user)
            {
                query = query.Include(x => x.User);
            }
            if (granter)
            {
                query = query.Include(x => x.Granter);
            }

            return base.Execute(query);
        }
    }
}

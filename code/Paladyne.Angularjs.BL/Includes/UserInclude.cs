using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.BL.Includes
{
    public class UserInclude : Include<User>
    {
        private bool userModules;
        private bool granter;

        public UserInclude UserModules(bool granter = false)
        {
            userModules = true;
            granter = granter;
            return this;
        }

        public override IQueryable<User> Execute(IQueryable<User> query)
        {
            if (userModules)
            {
                query = query.Include(x => x.UserModules);
            }
            if (granter)
            {
                query = query.Include(x => x.UserModules.Select(y => y.Granter));
            }

            return base.Execute(query);
        }
    }
}

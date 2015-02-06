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

        public UserInclude UserModules()
        {
            userModules = true;
            return this;
        }

        public override IQueryable<User> Execute(IQueryable<User> query)
        {
            if (userModules)
            {
                query = query.Include(x => x.UserModules);
            }

            return base.Execute(query);
        }
    }
}

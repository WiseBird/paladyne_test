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
        private UserModuleInclude userModules;
        public UserInclude UserModules(Action<UserModuleInclude> setup = null)
        {
            userModules = new UserModuleInclude();
            if (setup != null)
            {
                setup(userModules);
            }
            return this;
        }

        public override void Execute<TParentEntity>(Includer<TParentEntity, User> includer)
        {
            if (userModules != null)
            {
                includer.Include(x => x.UserModules, userModules);
            }

            base.Execute(includer);
        }
    }
}

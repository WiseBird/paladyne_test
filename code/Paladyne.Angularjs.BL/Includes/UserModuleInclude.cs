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

        public override void Execute<TParentEntity>(Includer<TParentEntity, UserModule> includer)
        {
            if (user)
            {
                includer.Include(x => x.User);
            }
            if (granter)
            {
                includer.Include(x => x.Granter);
            }

            base.Execute(includer);
        }
    }
}

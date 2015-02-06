using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;

namespace Paladyne.Angularjs.DAL.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            UserModules = new List<UserModule>( );
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastLogin { get; set; }

        public virtual List<UserModule> UserModules { get; set; } 
    }
}

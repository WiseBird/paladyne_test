using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            UserModules = new List<UserModule>();
            Granted = new List<UserModule>();
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? LastLogin { get; set; }

        public virtual ICollection<UserModule> UserModules { get; set; }
        public virtual ICollection<UserModule> Granted { get; set; }
    }
}

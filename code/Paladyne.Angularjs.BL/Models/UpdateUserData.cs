using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.BL.Models
{
    public class UpdateUserData : IValidatableObject
    {
        public class UserModule
        {
            [Required]
            public string Id { get; set; }
            public Permissions Permission { get; set; }
            public string GranterId { get; set; }
        }

        public UpdateUserData()
        {
            Modules = new List<UserModule>();
        }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public List<UserModule> Modules { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Modules != null && Modules.Count != 0)
            {
                foreach (var error in Modules.SelectMany(x => x.Validate()))
                {
                    yield return error;
                }
            }
        }
    }
}

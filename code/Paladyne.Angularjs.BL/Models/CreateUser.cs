﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.BL.Models
{
    public class CreateUser : IValidatableObject
    {
        public class UserModule
        {
            [Required]
            public string Id { get; set; }
            [Required]
            public string Name { get; set; }
            public Permissions Permission { get; set; }
            public string GranterId { get; set; }
        }

        public CreateUser()
        {
            Modules = new List<UserModule>();
        }

        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.Web.Models.Users
{
    public class UpdateUserData
    {
        public class UserModule
        {
            public string Id { get; set; }
            public Permissions Permission { get; set; }
        }

        public UpdateUserData()
        {
            Modules = new List<UserModule>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<UserModule> Modules { get; set; }
    }
}
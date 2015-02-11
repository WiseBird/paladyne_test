using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paladyne.Angularjs.Web.Models.Account
{
    public class RegisterUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
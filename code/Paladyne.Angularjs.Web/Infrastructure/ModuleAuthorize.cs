using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL.Entities;
using Paladyne.Angularjs.Web.Models;

using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace Paladyne.Angularjs.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ModuleAuthorizeAttribute : AuthorizeAttribute
    {
        public Modules Module { get; private set; }
        public Permissions Permission { get; private set; }

        public ModuleAuthorizeAttribute(Modules module)
            : this(module, Permissions.See)
        {
        }
        public ModuleAuthorizeAttribute(Modules module, Permissions permission)
        {
            Module = module;
            Permission = permission;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (!base.IsAuthorized(actionContext))
            {
                return false;
            }

            var userService = DependencyResolver.Current.GetService<IUserService>();
            var currentUser = userService.GetByNameEx(actionContext.ControllerContext.RequestContext.Principal.Identity.Name, new UserInclude().UserModules());
            if (currentUser == null)
            {
                return false;
            }

            if (!currentUser.UserModules.Any(x => x.ModuleId == Module.ToString() && x.Permission >= Permission))
            {
                return false;
            }

            return true;
        }
    }
}
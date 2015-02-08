using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Paladyne.Angularjs.Web.Controllers
{
    public class UsersController : ApiController
    {
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var userName = this.RequestContext.Principal.Identity.Name;
            return Ok(String.Format("Hello, {0}.", userName));
        }
    }
}

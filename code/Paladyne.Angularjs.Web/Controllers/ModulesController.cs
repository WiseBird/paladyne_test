using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Paladyne.Angularjs.Web.Infrastructure;
using Paladyne.Angularjs.Web.Models;

namespace Paladyne.Angularjs.Web.Controllers
{
    public class ModulesController : ApiController
    {
        [ModuleAuthorize(Modules.userModules)]
        public IHttpActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/module/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/module
        public void Post([FromBody]string value)
        {
        }

        // PUT api/module/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/module/5
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paladyne.Angularjs.Web.Infrastructure
{
    public class ServiceErrorsResult : JsonResult
    {
        public ServiceErrorsResult(object data, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet )
        {
            this.Data = data;
            this.JsonRequestBehavior = jsonRequestBehavior;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            context.HttpContext.Response.TrySkipIisCustomErrors = true;
            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }
    }
}
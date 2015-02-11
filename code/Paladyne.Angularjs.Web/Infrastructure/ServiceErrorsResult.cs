using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace Paladyne.Angularjs.Web.Infrastructure
{
    public class ServiceErrorsResult : JsonResult, IHttpActionResult
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

        public System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new JsonContent(Data);
            return Task.FromResult(response);
        }
    }

    public class JsonContent : HttpContent
    {
        private readonly MemoryStream _Stream = new MemoryStream();

        public JsonContent(object value)
        {

            var jw = new JsonTextWriter(new StreamWriter(_Stream)) { Formatting = Formatting.Indented };
            var serializer = new JsonSerializer();
            serializer.Serialize(jw, value);
            jw.Flush();
            _Stream.Position = 0;

        }
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            _Stream.CopyTo(stream);
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _Stream.Length;
            return true;
        }
    }
}
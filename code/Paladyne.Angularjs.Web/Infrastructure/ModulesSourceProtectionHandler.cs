using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.BL.Services;
using Paladyne.Angularjs.DAL;
using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.Web.Infrastructure
{
    public class ModulesSourceProtectionHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (!CheckAuth(context))
            {
                OnUnauthorized(context);
                return;
            }

            switch (context.Request.HttpMethod)
            {
                case "GET":
                    var filename = context.Server.MapPath(context.Request.Path);
                    SendContentTypeAndFile(context, filename);
                    break;
                default:
                    OnUnauthorized(context);
                    break;
            }
        }

        private static bool CheckAuth(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var userService = DependencyResolver.Current.GetService<IUserService>();
            var currentUser = userService.GetByNameEx(context.User.Identity.Name, new UserInclude().UserModules());
            if (currentUser == null)
            {
                return false;
            }

            var match = Regex.Match(context.Request.Path, @"/App/modules/([^/]*)/");
            if (!match.Success)
            {
                return false;
            }

            var moduleId = match.Groups[1].Value;
            var module = currentUser.UserModules.FirstOrDefault(x => x.ModuleId == moduleId);
            if (module == null)
            {
                return false;
            }

            if (module.Permission == Permissions.Prohibit)
            {
                return false;
            }

            return true;
        }

        private static void OnUnauthorized(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.End();
        }
        private static void OnNotFound(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.End();
        }

        private static void SendContentTypeAndFile(HttpContext context, String filename)
        {
            if (String.IsNullOrEmpty(filename))
            {
                OnNotFound(context);
                return;
            }

            var fileinfo = new System.IO.FileInfo(filename);
            if (!fileinfo.Exists)
            {
                OnNotFound(context);
                return;
            }

            var lastModified = fileinfo.LastWriteTimeUtc.ToString("R");
            if (lastModified == context.Request.Headers["If-Modified-Since"])
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                context.Response.End();
                return;
            }

            context.Response.AddHeader("Last-Modified", lastModified);
            context.Response.AddHeader("Cache-Control", "max-age=0");
            context.Response.ContentType = GetContentType(fileinfo);
            context.Response.TransmitFile(filename);
            context.Response.End();
        }

        private static string GetContentType(FileInfo fileinfo)
        {
            switch (fileinfo.Extension.ToLower())
            {
                case ".js":
                    return "application/javascript";
                    break;
                case ".html":
                    return "text/html";
                    break;
            }

            throw new Exception("Unknown file type");
        }
    }
}
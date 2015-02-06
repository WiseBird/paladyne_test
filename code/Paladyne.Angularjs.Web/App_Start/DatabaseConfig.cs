using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Paladyne.Angularjs.DAL;
using Paladyne.Angularjs.Web.Infrastructure;

namespace Paladyne.Angularjs.Web.App_Start
{
    public static class DatabaseConfig
    {
        public static void ConfigureDatabase()
        {
            Database.SetInitializer(new DataBaseInitializer());
            using (var context = new Context())
            {
                context.Database.Initialize(true);
            }

            DependencyResolver.Current.GetService<DataBaseFiller>().Fill();
        }
    }
}
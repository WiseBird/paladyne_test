using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Paladyne.Angularjs.DAL;
using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.Web.Infrastructure
{
    public class DataBaseInitializer : IDatabaseInitializer<Context>
    {
        public void InitializeDatabase(Context context)
        {
            if (!context.Database.Exists())
            {
                throw new Exception("Missing database");
            }

            if (!context.Database.CompatibleWithModel(true))
            {
                throw new Exception("Incompatible database");
            }
        }
    }
}
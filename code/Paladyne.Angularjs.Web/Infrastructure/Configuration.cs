using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Paladyne.Angularjs.Web.Infrastructure
{
    public class Configuration : IDBFillerConfiguration
    {
        private Configuration()
        { }

        private static Lazy<Configuration> configuration = new Lazy<Configuration>(() => new Configuration());
        public static Configuration Instance
        {
            get
            {
                return configuration.Value;
            }
        }

        private string adminUserName;
        public string AdminUserName
        {
            get
            {
                if (adminUserName == null)
                {
                    adminUserName = ConfigurationManager.AppSettings["AdminUserName"];
                }
                return adminUserName;
            }
        }

        private string adminPassword;
        public string AdminPassword
        {
            get
            {
                if (adminPassword == null)
                {
                    adminPassword = ConfigurationManager.AppSettings["AdminPassword"];
                }
                return adminPassword;
            }
        }
    }
}
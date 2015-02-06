using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paladyne.Angularjs.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappers()
        {
            BL.AutoMapperConfig.RegisterMappers();
        }
    }
}
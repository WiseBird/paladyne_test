using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paladyne.Angularjs.DAL.Tests
{
    public static class Helper
    {
        public static void Suppress(Action action)
        {
            try
            {
                action();
            }
            catch
            {
            }
        }
    }
}

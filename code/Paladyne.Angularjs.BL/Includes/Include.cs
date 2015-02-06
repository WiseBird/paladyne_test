using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paladyne.Angularjs.BL.Includes
{
    public class Include<T>
    {
        public static Include<T> None = new Include<T>();

        public virtual IQueryable<T> Execute(IQueryable<T> query)
        {
            return query;
        }
    }

    public static class IncludeExtensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> query, Include<T> include)
        {
            return include.Execute(query);
        }
    }
}

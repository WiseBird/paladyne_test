using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paladyne.Angularjs.BL
{
    public interface IValidationErrors
    {
        void Add(string property, string error);
        bool HasErrors();
    }
}

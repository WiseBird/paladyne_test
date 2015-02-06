using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Paladyne.Angularjs.BL;

namespace Paladyne.Angularjs.Web.Infrastructure
{
    public class ValidationErrors : IValidationErrors
    {
        private Action<string, string> onError = null;
        private bool hasErrors = false;

        public ValidationErrors(Action<string, string> onError)
        {
            this.onError = onError;
        }

        public void Add(string property, string error)
        {
            hasErrors = true;
            onError(property, error);
        }

        public bool HasErrors()
        {
            return hasErrors;
        }
    }
}
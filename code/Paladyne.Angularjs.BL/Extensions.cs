using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNet.Identity;

namespace Paladyne.Angularjs.BL
{
    public static class Extensions
    {
        public static List<ValidationResult> Validate(this object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(obj, context, results);
            return results;
        }

        public static bool Validate(this object obj, IValidationErrors errors)
        {
            var results = obj.Validate();
            if (results.Count == 0)
            {
                return true;
            }

            foreach (var validationResult in results)
            {
                errors.Add(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
            return false;
        }

        public static TTo MapTo<TFrom, TTo>(this TFrom from, TTo to = null)
            where TFrom : class, new()
            where TTo : class, new()
        {
            return Mapper.Map(from ?? new TFrom(), to ?? new TTo());
        }
        public static TTo MapTo<TTo>(this object from)
            where TTo : class
        {
            return Mapper.Map(from, from.GetType(), typeof(TTo)) as TTo;
        }
        public static IEnumerable<TTo> MapTo<TFrom, TTo>(this IEnumerable<TFrom> from)
            where TFrom : class, new()
            where TTo : class, new()
        {
            return from.Select(x => x.MapTo<TFrom, TTo>());
        }

        public static void AddErrorsFromResult(this IValidationErrors errors, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                errors.Add("", error);
            }
        }
    }
}

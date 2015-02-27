using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.DAL.Tests
{
    public static class ContextHelper
    {
        public static User AddUser(this IUnitOfWork context, Action<User> beforeSave = null)
        {
            var id = Guid.NewGuid().ToString();
            var entity = new User()
            {
                Id = id,
                UserName = id,
                FirstName = "FirstName",
                LastName = "LastName"
            };

            if (beforeSave != null)
            {
                beforeSave(entity);
            }

            context.Users.Add(entity);
            context.SaveChanges();
            return entity;
        }
        public static UserModule AddUserModule(this IUnitOfWork context, Action<UserModule> beforeSave = null)
        {
            var user = context.EnsureUser();
            var entity = new UserModule()
            {
                ModuleId = "ModuleId",
                ModuleName = "ModuleName",
                Permission = Permissions.Prohibit,
                User = user,
                UserId = user.Id,
            };

            if (beforeSave != null)
            {
                beforeSave(entity);
            }

            context.UserModules.Add(entity);
            entity.User.UserModules.Add(entity);

            context.SaveChanges();
            return entity;
        }

        public static User EnsureUser(this IUnitOfWork context, Action<User> beforeSave = null)
        {
            if (!context.Users.Any())
            {
                return context.AddUser(beforeSave);
            }

            return context.Users.OrderBy(x => x.Id).AsEnumerable().Last();
        }
        public static UserModule EnsureUserModule(this IUnitOfWork context, Action<UserModule> beforeSave = null)
        {
            if (!context.UserModules.Any())
            {
                return context.AddUserModule(beforeSave);
            }

            return context.UserModules.OrderBy(x => x.ModuleId).AsEnumerable().Last();
        }

        #region internal

        internal static void IterateDbSets(IUnitOfWork context, MethodInfo callback, object caller)
        {
            foreach (var property in typeof(IUnitOfWork).GetProperties())
            {
                if (!property.PropertyType.IsGenericType || property.PropertyType.GetGenericTypeDefinition() != typeof(IDbSet<>))
                {
                    continue;
                }

                var exp = Expression.MakeMemberAccess(Expression.Constant(context), property);

                callback.MakeGenericMethod(property.PropertyType.GetGenericArguments()[0])
                    .Invoke(caller, new object[] { exp });
            }
        }

        #endregion

        public static IUnitOfWork Create()
        {
#if DEBUG
            return FakeContext.Create();
#else
            return RealContextWrapper.Create();
#endif
        }
    }
}

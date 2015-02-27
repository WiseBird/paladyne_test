using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Paladyne.Angularjs.BL.Includes
{
    public class Include<T>
    {
        public static Include<T> None = new Include<T>();

        public virtual void Execute<TParentEntity>(Includer<TParentEntity, T> includer)
        { }
    }

    public class Includer<TParentEntity, TEntity>
    {
        public IQueryable<TParentEntity> Query { get; set; }
        private readonly string parentPath;

        public Includer(IQueryable<TParentEntity> query, string parentPath = null)
        {
            this.Query = query;
            this.parentPath = parentPath;
        }

        public void Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> ex, Include<TProperty> include = null)
        {
            this.Include(ex.Body, include);
        }
        public void Include<TProperty>(Expression<Func<TEntity, TProperty>> ex, Include<TProperty> include = null)
        {
            this.Include(ex.Body, include);
        }
        private void Include<TProperty>(Expression expression, Include<TProperty> include)
        {
            var path = expression.ParsePath();
            if (!string.IsNullOrEmpty(this.parentPath))
            {
                path = this.parentPath + "." + path;
            }
            this.Query = this.Query.Include(path);

            if (include != null)
            {
                this.Query = this.Query.Include(include, path);
            }
        }
    }

    public static class IncludeExtensions
    {
        public static IQueryable<T1> Include<T1, T2>(this IQueryable<T1> query, Include<T2> include, string parentPath = null)
        {
            var builder = new Includer<T1, T2>(query, parentPath);
            include.Execute(builder);
            return builder.Query;
        }

        public static Expression RemoveConvert(this Expression expression)
        {
            while ((expression.NodeType == ExpressionType.Convert) || (expression.NodeType == ExpressionType.ConvertChecked))
            {
                expression = ((UnaryExpression)expression).Operand;
            }
            return expression;
        }
        public static string ParsePath(this Expression expression)
        {
            Expression expression2 = RemoveConvert(expression);
            MemberExpression expression3 = expression2 as MemberExpression;
            MethodCallExpression expression4 = expression2 as MethodCallExpression;
            if (expression3 != null)
            {
                string name = expression3.Member.Name;
                string str2 = ParsePath(expression3.Expression);
                return (str2 == null) ? name : (str2 + "." + name);
            }
            else if (expression4 != null)
            {
                if ((expression4.Method.Name == "Select") && (expression4.Arguments.Count == 2))
                {
                    string str3 = ParsePath(expression4.Arguments[0]);
                    if (str3 != null)
                    {
                        LambdaExpression expression5 = expression4.Arguments[1] as LambdaExpression;
                        if (expression5 != null)
                        {
                            string str4 = ParsePath(expression5.Body);
                            if (str4 != null)
                            {
                                return str3 + "." + str4;
                            }
                        }
                    }
                }
                return null;
            }
            return null;
        }

    }
}

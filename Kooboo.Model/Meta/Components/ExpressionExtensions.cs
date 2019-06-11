using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kooboo.Model.Meta
{
    public static class ExpressionExtensions
    {
        public static string PropertyName<T>(this Expression<Func<T, object>> exp)
        {
            MemberExpression body = exp.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
            //return ((MemberExpression)exp.Body).Member.Name;
        }
    }
}

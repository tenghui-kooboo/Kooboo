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
            return ((MemberExpression)exp.Body).Member.Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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

        public static string Condition<T>(this Expression<Func<T,object>> exp)
        {
            UnaryExpression ubody = (UnaryExpression)exp.Body;
            if (ubody != null)
            {
                var filter = new ConditionFilter().ParseExpression(ubody.Operand);
                var builder = new StringBuilder();
                foreach(var item in filter.Items)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" && ");
                    }
                    builder.Append(item.GetFilter());
                }

                return builder.ToString();
            }

            return null;
        }

        

       
    }

    public class ConditionFilter
    {
        public List<ConditionFilterItem> Items = new List<ConditionFilterItem>();

        private void ParseAndAlso(Expression expression)
        {
            BinaryExpression binary = expression as BinaryExpression;

            ParseExpression(binary.Left);
            ParseExpression(binary.Right);

        }
        public ConditionFilter ParseExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.AndAlso)
            {
                ParseAndAlso(expression);
            }
            else
            {
                ParseComparer(expression);
            }
            return this;
        }
        private void ParseComparer(Expression expression)
        {
            BinaryExpression binary = expression as BinaryExpression;
            object name = GetLeftName(binary.Left);
            object constatvalue=GetRightName(binary.Right);

            this.Items.Add(new ConditionFilterItem()
            {
                Name=name.ToString(),
                Type=binary.NodeType,
                Value=constatvalue
            });
        }

        #region use on method
        private object GetLeftName(Expression expression)
        {
            object name = string.Empty;
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression member = expression as MemberExpression;
                name = UrlHelper.ToJsName(member.Member.Name);
            }
            else if (expression.NodeType == ExpressionType.Convert)
            {
                UnaryExpression unary = expression as UnaryExpression;
                MemberExpression member = unary.Operand as MemberExpression;
                name = UrlHelper.ToJsName(member.Member.Name);
            }
            else if (expression.NodeType == ExpressionType.Constant)
            {
                ConstantExpression value = expression as ConstantExpression;
                name = value.Value;
            }
            return name;
        }

        private object GetRightName(Expression expression)
        {
            object constatvalue = null;
            
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                constatvalue = Expression.Lambda<Func<object>>(Expression.Convert(expression, typeof(object))).Compile().Invoke();
            }
            else if (expression.NodeType == ExpressionType.Convert)
            {
                UnaryExpression unary = expression as UnaryExpression;
                MemberExpression member = unary.Operand as MemberExpression;

                constatvalue = Expression.Lambda<Func<object>>(Expression.Convert(member, typeof(object))).Compile().Invoke();
            }
            else if (expression.NodeType == ExpressionType.Constant)
            {
                ConstantExpression value = expression as ConstantExpression;
                constatvalue = value.Value;
            }

            return constatvalue;
        }
        #endregion

    }

    public class ConditionFilterItem
    {
        public string Name { get; set; }

        public ExpressionType Type { get; set; }

        public object Value { get; set; }

        public string GetFilter()
        {
            var compare = "";
            //temp only support equal and notequal
            
            switch (Type)
            {
                case ExpressionType.Equal:
                    compare = "==";
                    break;
                case ExpressionType.NotEqual:
                    compare = "!=";
                    break;
            }

            return string.Format("'{{{0}}}' {1} '{2}'", Name, compare, Value);
        }
    }
}

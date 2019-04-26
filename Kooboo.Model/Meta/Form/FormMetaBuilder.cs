using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;

namespace Kooboo.Model.Meta.Form
{
    public class FormMetaBuilder<T>
    {
        public FormMetaBuilder(FormMeta meta)
        {
            Meta = meta;
        }

        public FormMeta Meta { get; }

        public FormMetaBuilder<T> Item(Expression<Func<T, object>> getProperty, Action<FormItem> setItem)
        {
            Meta.Items.TryAdd(getProperty.PropertyName(), out FormItem item);
            setItem(item);

            return this;
        }

        public FormMetaBuilder<T> MergeModel(FormMetaOptions options)
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                Meta.Items.TryAdd(property.Name, out FormItem item);

                var attrs = property.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    options.MapAttribute(attr, item);
                }
            }

            return this;
        }
    }

    public static class FormMetaBuilderExtensions
    {
        public static FormMetaBuilder<T> Builder<T>(this FormMeta meta)
        {
            return new FormMetaBuilder<T>(meta);
        }

        public static FormMetaBuilder<T> MergeModel<T>(this FormMetaBuilder<T> builder)
        {
            return builder.MergeModel(MetaOptions.Instance.Form);
        }
    }
}

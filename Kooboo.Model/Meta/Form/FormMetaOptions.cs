using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Kooboo.Model.Meta.Form
{
    public class FormMetaOptions
    {
        public delegate void AttributeMapDelegate(object attr, FormItem item);

        private Dictionary<Type, AttributeMapDelegate> AttributeMap { get; } = new Dictionary<Type, AttributeMapDelegate>();

        public FormMetaOptions AddAttributeMap<T>(Action<T, FormItem> map)
            where T : Attribute
        {
            AttributeMap.Add(typeof(T), (attr, item) => map((T)attr, item));
            return this;
        }

        public void MapAttribute(object attr, FormItem item)
        {
            var type = attr.GetType();
            if (typeof(Validation.ValidationRule).IsAssignableFrom(type))
            {
                item.Rules.Add(attr as Validation.ValidationRule);
            }
            else
            {
                if (!AttributeMap.TryGetValue(attr.GetType(), out AttributeMapDelegate map))
                    return;

                map(attr, item);
            }
        }

        public static FormMetaOptions Default()
        {
            return new FormMetaOptions()
                .AddAttributeMap<DisplayFormatAttribute>((attr, item) => item.Placeholder = attr.NullDisplayText)
                .AddAttributeMap<UIHintAttribute>((attr, item) => item.Type = attr.UIHint)
                .AddAttributeMap<DisplayAttribute>((attr, item) =>
                {
                    item.Label = new Localizable(attr.Name);
                    item.Tooltip = new Localizable(attr.Description);
                });
        }
    }
}

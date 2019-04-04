using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Kooboo.Model.Render.Vue;

namespace Kooboo.Model.Render
{
    public class VueJsBuilder : IJsBuilder
    {
        private static Dictionary<Type, IVueRenderer> _renderers = new Dictionary<Type, IVueRenderer>
        {
            { typeof(Vue.Data), new DataRenderer() },
            { typeof(Vue.Hook), new HookRenderer() },
            { typeof(Vue.Method), new MethodRenderer() },
            { typeof(Vue.El), new ElRenderer() },
            { typeof(Vue.Validation), new ValidationRenderer() },
        };

        private Dictionary<Type, List<object>> _items = new Dictionary<Type, List<object>>();

        public IJsBuilder AddItem(object item)
        {
            var type = item.GetType();
            if (!_items.TryGetValue(type, out List<object> group))
            {
                if (!_renderers.ContainsKey(type))
                    throw new ArgumentException($"Item type {type.Name} not supported", nameof(item));

                group = new List<object>();
                _items.Add(type, group);
            }

            group.Add(item);
            return this;
        }

        public string Build()
        {
            var builder = new StringBuilder()
                //.AppendLine("var app = new Vue({");
                .AppendLine("Kooboo.Vue.extend({");

            int i = 0;
            foreach (var each in _items)
            {
                if (i > 0)
                {
                    builder.AppendLine(",");
                }
                _renderers[each.Key].Render(builder, each.Value);
                i++;
            }

            builder.AppendLine().Append("})");
            builder.AppendLine().Append("Kooboo.Vue.execute();");
            return builder.ToString();
        }
    }
}

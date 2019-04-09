using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Kooboo.Model.Render.Vue;

namespace Kooboo.Model.Render.Vue
{
    public class SubViewJsBuilder : IJsBuilder
    {
        private List<object> _items = new List<object>();

        public SubViewJsBuilder(VueJsBuilderOptions options)
        {
            Options = options;
        }

        public VueJsBuilderOptions Options { get; }

        public IJsBuilder AddItem(object item)
        {
            _items.Add(item);

            return this;
        }

        public string Build()
        {
            var builder = new InnerJsBuilder(null);

            VueJsHelper.Build(builder, _items, Options);

            return builder.ToString();
        }

        public string Build(string template)
        {
            var builder = new InnerJsBuilder(null);
            
            VueJsHelper.Build(builder, _items, Options);

            builder.DirectRender(b => b.Append($"template: '{template}'"));

            return builder.Build();
        }
    }
}

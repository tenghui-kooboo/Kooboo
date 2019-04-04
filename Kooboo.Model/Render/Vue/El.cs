using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kooboo.Model.Render.Vue
{
    public class El
    {
        public string Name { get; set; }
    }

    public class ElRenderer : IVueRenderer
    {
        public void Render(StringBuilder builder, IEnumerable<object> items)
        {
            var item = items.FirstOrDefault() as El;
            if (item == null)
                throw new ArgumentException("Contains at least one item", nameof(items));

            builder.Append($"el: '{item.Name}'");
        }
    }
}

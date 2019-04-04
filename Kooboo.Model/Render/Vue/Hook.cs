using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Render.Vue
{
    public class Hook : NameBody
    {

    }

    public class HookRenderer : IVueRenderer
    {
        public void Render(StringBuilder builder, IEnumerable<object> items)
        {
            int i = 0;
            foreach (Hook item in items)
            {
                if (i > 0)
                {
                    builder.AppendLine(",");
                }
                builder.Append($"{item.Name}: function() {{ {item.Body} }}");
                i++;
            }
        }
    }
}

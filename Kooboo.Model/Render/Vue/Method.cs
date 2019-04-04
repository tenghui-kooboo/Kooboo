using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Render.Vue
{
    public class Method:NameBody
    {
        
    }

    public class MethodRenderer : IVueRenderer
    {
        public void Render(StringBuilder builder, IEnumerable<object> items)
        {
            builder.AppendLine("methods: {");
            int i = 0;
            foreach (Method item in items)
            {
                if (i > 0)
                {
                    builder.AppendLine(",");
                }
                builder.Append($"  {item.Name}: function() {{ {item.Body} }}");
                i++;
            }
            builder.AppendLine().Append("}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Render.Vue
{
    public class Data : NameBody
    {
    }

    public class DataRenderer : IVueRenderer
    {
        public void Render(StringBuilder builder, IEnumerable<object> items)
        {
            builder.AppendLine("data: {");
            int i = 0;
            foreach (Data item in items)
            {
                if (i > 0)
                {
                    builder.AppendLine(",");
                }
                builder.Append($"  {item.Name}: {item.Body}");
                i++;
            }
            builder.AppendLine().Append("}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Render.Vue
{
    public partial class Data
    {
        public string Name { get; set; }

        public string Json { get; set; }
    }

    partial class Data
    {
        public class Renderer : IVueRenderer
        {
            public void Render(InnerJsBuilder builder, IEnumerable<object> items, VueJsBuilderOptions options)
            {
                foreach (Data item in items)
                {
                    builder.Data(b =>
                    {
                        b.Append($"{item.Name}: {item.Json}");
                    });
                }
            }
        }
    }
}

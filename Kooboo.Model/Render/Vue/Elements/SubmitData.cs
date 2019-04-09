using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Render.Vue
{
    public partial class SubmitData
    {
        public string Url { get; set; }

        public string ModelName { get; set; }
    }

    partial class SubmitData
    {
        public const string Keyword_Submit = "submit";
        public const string Keyword_ApiPost = "api.post";

        public class Renderer : IVueRenderer
        {
            public void Render(InnerJsBuilder builder, IEnumerable<object> items, VueJsBuilderOptions options)
            {
                foreach (SubmitData item in items)
                {
                    builder.Methods(b =>
                    {
                        b.AppendLine($"{Keyword_Submit}_{item.ModelName}: function() {{").Indent();

                        b.AppendLine($"const url = {LoadData.Keyword_ParameterBind}('{item.Url}')");
                        b.AppendLine($"{Keyword_ApiPost}(url, this.{item.ModelName})");

                        b.Unindent().Append("}");
                    });
                }
            }
        }
    }
}

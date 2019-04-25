using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Meta.Validation;
using Newtonsoft.Json;

namespace Kooboo.Model.Render.Vue
{
    public partial class Validation
    {
        public string Name { get; set; }

        public IEnumerable<Meta.Api.PropertyInfo> Properties { get; set; }
    }

    partial class Validation
    {
        public class Renderer : IVueRenderer
        {
            public void Render(InnerJsBuilder builder, IEnumerable<object> items, VueJsBuilderOptions options)
            {
                foreach (Validation item in items)
                {   
                    builder.Validations(b =>
                    {
                        b.AppendLine($"{ParserHelper.ToJsName(item.Name)}: {{").Indent();
                        int j = 0;
                        foreach (var prop in item.Properties)
                        {
                            if (j > 0)
                            {
                                b.AppendLine(",");
                            }

                            b.AppendLine($"{ParserHelper.ToJsName(prop.Name)}: [").Indent();
                            int i = 0;
                            foreach (var rule in prop.Rules)
                            {
                                if (i > 0)
                                {
                                    b.AppendLine(",");
                                }
                                b.Append(rule.ToJson());

                                i++;
                            }
                            b.AppendLine().Unindent().Append("]");

                            j++;
                        }

                        b.AppendLine().Unindent().Append("}");
                    });
                }
            }
        }
    }
}

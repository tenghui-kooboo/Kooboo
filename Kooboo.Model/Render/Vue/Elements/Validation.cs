using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.ValidateRules;

namespace Kooboo.Model.Render.Vue
{
    public partial class Validation
    {
        public string Name { get; set; }

        public List<Rule> Rules { get; set; }
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
                        b.AppendLine($"{item.Name}: {{").Indent();
                        b.AppendLine("rule:ruleValid([").Indent();

                        int i = 0;
                        foreach (var rule in item.Rules)
                        {
                            if (i > 0)
                            {
                                b.Append(",");
                            }
                            b.Append(rule.GetRule());
                            i++;
                        }

                        b.Unindent().AppendLine("])");
                        b.Unindent().Append("}");
                    });
                }
            }
        }
    }
}

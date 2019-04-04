using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.ValidateRules;

namespace Kooboo.Model.Render.Vue
{
    public class Validation
    {
        public string Name { get; set; }

        public List<Rule> Rules { get; set; }
    }

    public class ValidationRenderer : IVueRenderer
    {
        public void Render(StringBuilder builder, IEnumerable<object> items)
        {
            if (items.Count() > 0)
            {
                builder.Append("validations:{");
                
                foreach(Validation item in items)
                {
                    builder.Append($"{item.Name}:{{");

                    builder.Append("rule:ruleValid([");
                    int i = 0;
                    foreach(var rule in item.Rules)
                    {
                        if (i > 0)
                        {
                            builder.Append(",");
                        }
                        builder.Append(rule.GetRule());
                        i++;
                    }
                    builder.Append("])");

                    builder.Append("},");
                }

                builder.Append("}");
            }
        }
    }
}

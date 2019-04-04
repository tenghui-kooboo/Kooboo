using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.ValidateRules;

namespace Kooboo.Model
{
    public class VueField
    {
        public string Name { get; set; }

        public object Value { get; set; }//type

        public List<Rule> ValidateRules { get; set; } = new List<Rule>();

        public int TabCount { get; set; }

        public string GetDataValue()
        {
            var sb = new StringBuilder();
            var valueStr = Kooboo.Lib.Helper.JsonHelper.Serialize(Value);

            sb.Append(valueStr);
            sb.Append(",");
            //sb.AppendTabs(TabCount, Value);
            return sb.ToString();
        }

        public string GetRules()
        {
            var sb = new StringBuilder();
            #region rules
            if(ValidateRules.Count>0)
            {
                sb.AppendLine("rule:ruleValid([");
                for (var i = 0; i < ValidateRules.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                        sb.AppendLine();
                    }

                    var rule = ValidateRules[i];
                    sb.Append(rule.GetRule());
                }
                sb.AppendLine("])");
            }
           
            #endregion
           
            return sb.ToString();
        }


    }
}

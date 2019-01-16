using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.ValidateRules
{
    public class RegexRule:Rule
    {
        public string Regex { get; set; }
        public RegexRule(string regex,string message)
        {
            Regex = regex;
            Message = message;
        }

        public override string GetRule()
        {
            return "{}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class RegexRule : ValidationRule
    {
        public RegexRule(string regex)
            : base("regex")
        {
            Regex = regex;
            Message = "Invalid format";
        }

        public string Regex { get; set; }
    }
}

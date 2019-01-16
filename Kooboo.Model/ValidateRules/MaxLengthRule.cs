using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.ValidateRules
{
    public class MaxLengthRule:Rule
    {
        public int MaxLength;

        public string Trigger { get; set; } = "blur";

        public MaxLengthRule(int maxLength,string message)
        {
            MaxLength = maxLength;
            Message = string.Format(message,MaxLength).Replace("\"", "\\\"");
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"maxLength\",message:\"{0}\"}}", Message);
        }
    }
}

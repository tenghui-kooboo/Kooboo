using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LabelText : Attribute, IMetaAttribute
    {
        public string PropertyName => "text";

        public bool IsHeader => false;

        public string TrueText { get; set; }

        public string FalseText { get; set; }
        public LabelText(string trueText, string falseText)
        {
            TrueText = trueText;
            FalseText = falseText;
        }

        public string Value()
        {
            return $"{{true:\"{TrueText}\",false:\"{FalseText}\"}}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LabelClass : Attribute, IMetaAttribute
    {
        public string PropertyName => "class";

        public bool IsHeader => false;

        public string ClassName { get; set; }
        public string TrueClass { get; set; }

        public string FalseClass { get; set; }
        public LabelClass(string classname,string trueClass,string falseClass)
        {
            ClassName = classname;
            TrueClass = trueClass;
            FalseClass = falseClass;
        }

        public string Value()
        {
            return $"[\"{ClassName}\",{{true:\"{TrueClass}\",false:\"{FalseClass}\"}}]";
        }
    }
}

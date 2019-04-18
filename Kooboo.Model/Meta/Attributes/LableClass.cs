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

        public object Value()
        {
            var list = new List<object>();
            list.Add(ClassName);
            var dic = new Dictionary<bool, string>();
            dic.Add(true, TrueClass);
            dic.Add(false, FalseClass);
            list.Add(dic);

            return list;
        }
    }
}

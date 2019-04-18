using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VisibleAttribute : Attribute, IMetaAttribute
    {
        public CompareOperation CompareOperation { get; set; }

        public int CompareValue { get; set; }

        public VisibleAttribute(CompareOperation compareOperation,int value)
        {
            CompareOperation = compareOperation;
            CompareValue = value;
        }

        public bool IsHeader => false;

        public string PropertyName => "displayName";

        public object Value()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("compare", CompareOperation.ToString());
            dic.Add("value", CompareValue);

            return dic;
        }
    }

    public enum CompareOperation
    {
        lt=0,
        elt=1,
        gt=2,
        egt=3
    }
}

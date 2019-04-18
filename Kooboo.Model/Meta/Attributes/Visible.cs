using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VisibleAttribute : Attribute, IMetaAttribute
    {
        public CompareOperation Operation { get; set; }

        public int CompareValue { get; set; }

        public VisibleAttribute(CompareOperation compareOperation,int value)
        {
            Operation = compareOperation;
            CompareValue = value;
        }

        public bool IsHeader => false;

        public string PropertyName => "visible";

        public object Value()
        {
            var dic = new Dictionary<string, object>();
           
            dic.Add("compare", GetOperation(Operation));
            dic.Add("value", CompareValue);

            return dic;
        }

        private string GetOperation(CompareOperation operation)
        {
            var memInfo = typeof(CompareOperation).GetMember(Operation.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(OperationAttribute), false);
            var attr = attributes[0] as OperationAttribute;
            return attr.Operation;
        }

       

        

    }
    public class OperationAttribute : Attribute
    {
        public string Operation { get; set; }
        public OperationAttribute(string operation)
        {
            Operation = operation;
        }
    }

    public enum CompareOperation
    {
        [Operation("==")]
        Equal,
        [Operation(">")]
        LessThan,
        [Operation("<=")]
        EqualOrLessThan,
        [Operation(">")]
        GreaterThan,
        [Operation(">=")]
        EqualOrGreaterThan
    }



}

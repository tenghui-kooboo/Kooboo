using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Definition;

namespace Kooboo.Model.Meta.Attributes
{ 
    [AttributeUsage(AttributeTargets.Property)]
    public class ActionType : Attribute, IMetaAttribute
    {
        public EnumActionType actionType { get; set; }

        public ActionType(EnumActionType action)
        {
            this.actionType = action; 
        }

        public bool IsHeader => false;

        public string PropertyName => "Action";

        public string Value()
        {
            return MetaHelper.ToCamalCaseName(this.actionType.ToString()); 
        }
    }
}

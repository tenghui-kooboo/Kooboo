using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{ 
    [AttributeUsage(AttributeTargets.Property)]
    public class CssClass : Attribute, IMetaAttribute
    {
        public string classname { get; set; }

        public bool IsHeader => false; 

        public string PropertyName => "class";

        public CssClass(string classname)
        {
            this.classname = classname;
        }

        public object Value()
        {
            return this.classname; 
        }
    } 
}
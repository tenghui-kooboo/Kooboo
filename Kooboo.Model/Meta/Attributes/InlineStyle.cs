using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
   
    [AttributeUsage(AttributeTargets.Property)]
    public class InlineStyle : Attribute, IMetaAttribute
    {
        public string inline  { get; set; }

        public bool IsHeader => false;

        public string PropertyName => "style";

        public InlineStyle(string inlineStyle)
        {
            this.inline = inlineStyle; 
        }

        public object Value()
        {
            return this.inline; 
        }
    }  
}

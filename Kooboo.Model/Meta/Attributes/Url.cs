using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UrlAttribute : Attribute, IMetaAttribute
    {
        private string Url { get; set; }
        public UrlAttribute(string url)
        {
            this.Url = url;
        }

        public bool IsHeader => false;

        public string PropertyName => "url";

        public object Value()
        {
            return Url;
        }
    }
}

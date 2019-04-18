using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TitleAttribute : Attribute, IMetaAttribute
    {
        public string PropertyName => "title";

        public bool IsHeader => false;

        public string  Title { get; set; }

        public TitleAttribute(string title)
        {
            Title = title;
        }

        public object Value()
        {
            return Title;
        }

    }
}

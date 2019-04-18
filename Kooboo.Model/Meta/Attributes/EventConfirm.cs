using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EventConfirm : Attribute, IMetaAttribute
    {
        public string Confirm { get; set; }

        public bool IsHeader => false;

        public string PropertyName => "confirm";

        public EventConfirm(string confirm)
        {
            Confirm = confirm;
        }

        public string Value()
        {
            return Confirm;
        }
    }
}

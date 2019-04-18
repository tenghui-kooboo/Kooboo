using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormTypeAttribute : Attribute, IMetaAttribute
    {
        public string PropertyName => "layout";

        public bool IsHeader => false;

        public EnumFormType FormType { get; set; }

        public FormTypeAttribute(EnumFormType formType)
        {
            FormType = formType;
        }

        public string Value()
        {
            return FormType.ToString();
        }

    }

    public enum EnumFormType
    {
        checkbox=0,
        datetime,
        number,
        radiobox,
        richeditor,
        selection,
        switchType,
        textarea,
        textbox,
        upload
    }

}

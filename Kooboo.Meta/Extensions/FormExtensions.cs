using Kooboo.Meta.Views;
using Kooboo.Meta.Views.FormFields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class FormExtensions
    {
        public static TextField AddText(this KbForm kbForm, Action<TextField> action)
        {
            var field = new TextField();
            action(field);
            kbForm.Fields.Add(field);
            return field;
        }

        public static RadioField AddRadio(this KbForm kbForm, Action<RadioField> action)
        {
            var field = new RadioField();
            action(field);
            kbForm.Fields.Add(field);
            return field;
        }

        public static FileField AddFile(this KbForm kbForm, Action<FileField> action)
        {
            var field = new FileField();
            action(field);
            kbForm.Fields.Add(field);
            return field;
        }
    }
}

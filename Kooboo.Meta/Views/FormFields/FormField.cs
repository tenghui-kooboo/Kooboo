using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views.FormFields
{
    public abstract class FormField : KbView
    {
        public string Label { get; set; }
        public bool Visible { get; set; } = true;
        public string Field { get; set; }
        public string DefaultValue { get; set; }
        public string Placeholder { get; set; }
    }

}

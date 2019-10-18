using Kooboo.Meta.Views.Abstracts;
using Kooboo.Meta.Views.FormFields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{

    public class KbForm : KbView
    {
        public override string Name => nameof(KbForm);

        public List<FormField> Fields { get; set; } = new List<FormField>();
    }
}

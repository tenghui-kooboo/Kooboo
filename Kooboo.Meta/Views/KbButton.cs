using Kooboo.Meta.Models;
using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbButton : KbClickable
    {
        public override string Name => nameof(KbButton);

        public string Text { get; set; }

        public string Color { get; set; }

        public string Icon { get; set; }

        public bool Visible { get; set; } = true;
    }
}

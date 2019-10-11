using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbIcon : KbView
    {
        public override string Name => nameof(KbIcon);

        public string IconName { get; set; }

        public bool Visible { get; set; } = true;
    }
}

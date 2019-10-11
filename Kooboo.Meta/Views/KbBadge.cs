using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbBadge : KbView
    {
        public override string Name => nameof(KbBadge);

        public string Text { get; set; }
    }
}

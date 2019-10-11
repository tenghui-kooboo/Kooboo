using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbText : KbView
    {
        public override string Name => nameof(KbText);

        public string Text { get; set; }
    }
}

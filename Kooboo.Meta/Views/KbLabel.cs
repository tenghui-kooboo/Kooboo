using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbLabel : KbClickable
    {
        public override string Name => nameof(KbLabel);

        public string Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbButton : View
    {
        public override string Name => nameof(KbButton);

        public string Text { get; set; }

    }
}

using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class ButtonExtensions
    {

        public static KbButton PullRight(this KbButton kbButton)
        {
            kbButton.AddClass("pull-right");
            return kbButton;
        }
    }
}

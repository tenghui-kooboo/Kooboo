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

        public static KbButton ShowPopup(this KbButton kbButton,string popupId)
        {
            kbButton.Execute($"k.pool.{popupId}.visible=true");
            return kbButton;
        }

        public static KbButton ClosePopup(this KbButton kbButton, string popupId)
        {
            kbButton.Execute($"k.pool.{popupId}.visible=false");
            return kbButton;
        }
    }
}

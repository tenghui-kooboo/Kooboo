using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class PopupExtensions
    {
        public static KbButton AddFooterButton(this KbPopup popup, Action<KbButton> action)
        {
            var btn = new KbButton();
            action(btn);
            popup.Footer.Add(btn);
            return btn;
        }
    }
}

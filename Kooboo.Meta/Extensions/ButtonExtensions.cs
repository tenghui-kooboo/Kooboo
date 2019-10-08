using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class ButtonExtensions
    {
        public static KbButton AddClass(this KbButton kbButton, string @class)
        {
            kbButton.AddHook("load", kbButton.Id, $"k.self.classList.push('{@class}')");
            return kbButton;
        }

        public static KbButton SetBlue(this KbButton kbButton)
        {
            kbButton.AddClass("blue");
            return kbButton;
        }

        public static KbButton SetRed(this KbButton kbButton)
        {
            kbButton.AddClass("red");
            return kbButton;
        }

        public static KbButton SetGreen(this KbButton kbButton)
        {
            kbButton.AddClass("green");
            return kbButton;
        }

        public static KbButton PullRight(this KbButton kbButton)
        {
            kbButton.AddClass("pull-right");
            return kbButton;
        }

        public static KbButton SetIcon(this KbButton kbButton, string iconClass)
        {
            kbButton.AddHook("load", kbButton.Id, $"k.self.icon='{iconClass}'");
            return kbButton;
        }

    }
}

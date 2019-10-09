using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class DropdownExtensions
    {
        public static KbDropdown AddItem(this KbDropdown dropdown, Action<KbDropdown.Item> action)
        {
            var item = new KbDropdown.Item();
            action(item);
            dropdown.Items.Add(item);
            return dropdown;
        }

        public static KbDropdown SetItemTemplate(this KbDropdown dropdown, Action<KbDropdown.Template> action)
        {
            var template = new KbDropdown.Template();
            action(template);
            dropdown.ItemTemplate = template;
            return dropdown;
        }

        public static KbDropdown AddClass(this KbDropdown kbButton, string @class)
        {
            kbButton.AddHook("load", kbButton.Id, $"k.self.classList.push('{@class}')");
            return kbButton;
        }

        public static KbDropdown SetBlue(this KbDropdown kbButton)
        {
            kbButton.AddClass("blue");
            return kbButton;
        }

        public static KbDropdown SetRed(this KbDropdown kbButton)
        {
            kbButton.AddClass("red");
            return kbButton;
        }

        public static KbDropdown SetGreen(this KbDropdown kbButton)
        {
            kbButton.AddClass("green");
            return kbButton;
        }
    }
}

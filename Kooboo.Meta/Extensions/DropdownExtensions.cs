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

        public static KbDropdown AddItemTemplate(this KbDropdown dropdown, Action<KbDropdown.Item> action)
        {
            var item = new KbDropdown.Item();
            action(item);
            dropdown.ItemTemplate = item;
            return dropdown;
        }
    }
}

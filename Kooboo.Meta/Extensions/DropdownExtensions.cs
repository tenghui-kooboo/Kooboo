using Kooboo.Meta.Models;
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

        public static KbDropdown SetData(this KbDropdown dropdown, string id, JsCode source)
        {
            dropdown.AddHook("dataLoad", id, $"k.self.items={source};");
            return dropdown;
        }

    }
}

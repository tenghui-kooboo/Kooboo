using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views.FormFields
{
    public class RadioField : FormField
    {
        public class Item
        {
            public string Label { get; set; }
            public object Value { get; set; }
        }

        public override string Name => nameof(RadioField);

        public List<Item> Items { get; private set; } = new List<Item>();

        public Item AddItem(Action<Item> action)
        {
            var item = new Item();
            action(item);
            this.Items.Add(item);
            return item;
        }
    }
}

using Kooboo.Meta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbDropdown : KbView
    {
        public class Item : KbButton
        {
            public override string Name => nameof(Item);
        }

        public override string Name => nameof(KbDropdown);

        public string Text { get; set; }

        public List<Item> Items { get; private set; } = new List<Item>();

        public Item ItemTemplate { get; internal set; }
    }
}

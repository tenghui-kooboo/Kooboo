using Kooboo.Meta.Views;
using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class ContainerExtensions
    {
        public static KbView AddView<T>(this T container, KbView view) where T : KbContainer
        {
            container.Views.Add(view);
            return container;
        }

        public static KbButton AddButton<T>(this T container, Action<KbButton> action) where T : KbContainer
        {
            var btn = new KbButton();
            action(btn);
            container.AddView(btn);
            return btn;
        }

        public static KbText AddText<T>(this T container, Action<KbText> action) where T : KbContainer
        {
            var text = new KbText();
            action(text);
            container.AddView(text);
            return text;
        }

        public static KbLabel AddLabel<T>(this T container, Action<KbLabel> action) where T : KbContainer
        {
            var label = new KbLabel();
            action(label);
            container.AddView(label);
            return label;
        }

        public static KbBadge AddBadge<T>(this T container, Action<KbBadge> action) where T : KbContainer
        {
            var badge = new KbBadge();
            action(badge);
            container.AddView(badge);
            return badge;
        }

        public static KbIcon AddIcon<T>(this T container, Action<KbIcon> action) where T : KbContainer
        {
            var icon = new KbIcon();
            action(icon);
            container.AddView(icon);
            return icon;
        }

        public static KbDropdown AddDropdown<T>(this T container, Action<KbDropdown> action) where T : KbContainer
        {
            var dropdown = new KbDropdown();
            action(dropdown);
            container.AddView(dropdown);
            return dropdown;
        }

        public static KbNavbar AddKbNavbar<T>(this T container, Action<KbNavbar> action) where T : KbContainer
        {
            var bar = new KbNavbar();
            action(bar);
            container.AddView(bar);
            return bar;
        }

        public static KbTable AddTable<T>(this T container, Action<KbTable> action) where T : KbContainer
        {
            var table = new KbTable();
            action(table);
            container.AddView(table);
            return table;
        }

        public static KbPopup AddPopup<T>(this T container, Action<KbPopup> action) where T : KbContainer
        {
            var popup = new KbPopup();
            action(popup);
            container.AddView(popup);
            return popup;
        }
    }
}

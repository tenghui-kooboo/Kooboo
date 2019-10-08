using Kooboo.Meta.Views;
using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class ContainerExtensions
    {
        public static KbButton AddButton<T>(this T container, Action<KbButton> action) where T : KbContainer
        {
            var btn = new KbButton();
            action(btn);
            container.AddView(btn);
            return btn;
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
    }
}

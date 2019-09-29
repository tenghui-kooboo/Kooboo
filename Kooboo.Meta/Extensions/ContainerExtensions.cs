using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class ContainerExtensions
    {
        public static KbButton AddButton<T>(this T container, Action<KbButton> action) where T : Container
        {
            var btn = new KbButton();
            action(btn);
            container.AddView(btn);
            return btn;
        }

        public static KbNavbar AddKbNavbar<T>(this T container, Action<KbNavbar> action) where T : Container
        {
            var bar = new KbNavbar();
            action(bar);
            container.AddView(bar);
            return bar;
        }
    }
}

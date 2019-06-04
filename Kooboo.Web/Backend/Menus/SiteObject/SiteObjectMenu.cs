using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Web.Menus.ObjectMenu
{
    public class SiteObjectMenu : Attribute
    {
        public string Text { get; set; }

        public bool AlignRight { get; set; }

        public string Style { get; set; }

        public string Class { get; set; }

        public bool Multiple { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Kooboo.Model.Meta.Table
{
    public class Header
    {
        public string Text { get; set;  }

        public ClassMeta Class { get; set; }

        public string Style { get; set; }
    }
}

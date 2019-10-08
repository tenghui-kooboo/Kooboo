using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbTable : KbView
    {

        public class Column : KbView
        {
            public override string Name => nameof(Column);
            public string Text { get; set; }
        }

        public override string Name => nameof(KbTable);
    }
}

using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbTable : KbView
    {


        public class Cell : KbContainer
        {
            public class Template : KbView
            {
                public KbView View { get; set; }

                public override string Name => nameof(Template);
            }

            public override string Name => nameof(Cell);

            public string Text { get; set; }

            public Template ItemTemplate { get; set; }
        }

        public class Row : KbView
        {
            public override string Name => nameof(Row);

            public List<Cell> Cells { get; set; } = new List<Cell>();
        }

        public bool ShowCheck { get; set; }

        public override string Name => nameof(KbTable);

        public Row RowTemplate { get; set; }
    }
}

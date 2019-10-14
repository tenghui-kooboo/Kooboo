using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{

    public class Field : KbView
    {
        public override string Name => nameof(Field);

        public string Label { get; set; }

        public string Placeholder { get; set; }
    }

    public class KbForm : KbView
    {
        public override string Name => nameof(KbForm);

        public List<Field> Fields { get; set; } = new List<Field>();
    }
}

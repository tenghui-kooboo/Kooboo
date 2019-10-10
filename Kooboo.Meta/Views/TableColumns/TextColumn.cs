using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views.TableColumns
{
    public class TextColumn : KbView
    {
        public string Text { get; set; }

        public override string Name => throw new NotImplementedException();
    }
}

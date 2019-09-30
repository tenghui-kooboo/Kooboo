using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbContainer : KbView
    {
        public override string Name => nameof(KbContainer);
        public List<KbView> Views { get; set; } = new List<KbView>();

        public KbView AddView(KbView view)
        {
            Views.Add(view);
            return view;
        }
    }
}

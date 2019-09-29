using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class Container : View
    {
        public override string Name => nameof(Container);
        public List<View> Views { get; set; } = new List<View>();

        public View AddView(View view)
        {
            Views.Add(view);
            return view;
        }
    }
}

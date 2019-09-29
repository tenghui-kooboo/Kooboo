using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views.Internal
{
    internal class Meta : View, IMeta
    {
        string _name = null;
        public List<View> Views { get; set; } = new List<View>();

        public override string Name => _name;

        public View AddView(View view)
        {
            Views.Add(view);
            return view;
        }

        public string SetRoute(string name)
        {
            _name = name;
            return name;
        }

    }
}

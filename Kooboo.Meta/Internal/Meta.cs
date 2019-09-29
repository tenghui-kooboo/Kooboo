using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Internal
{
    internal class Meta : IMeta, IView
    {
        public string Name { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public List<IView> Views { get; set; } = new List<IView>();

        public IView AddView(IView view)
        {
            Views.Add(view);
            return view;
        }

        public string SetRoute(string name)
        {
            Name = name;
            return name;
        }

    }
}

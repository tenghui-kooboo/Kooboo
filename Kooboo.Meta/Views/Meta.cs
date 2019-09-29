using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class Meta : Container
    {
        string _name = null;

        public override string Name => _name;

        public string SetRoute(string name)
        {
            _name = name;
            return name;
        }

    }
}

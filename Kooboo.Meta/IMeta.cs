using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public interface IMeta
    {
        string SetRoute(string name);

        IView AddView(IView view);
    }
}

using System;
using System.Text;
using System.Collections.Generic;

namespace Kooboo.Model.Render.Vue
{
    public interface IVueRenderer
    {
        void Render(StringBuilder builder, IEnumerable<object> items);
    }
}

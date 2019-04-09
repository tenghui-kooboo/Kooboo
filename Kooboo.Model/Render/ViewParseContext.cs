using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kooboo.Dom;
using Kooboo.Data.Context;

namespace Kooboo.Model.Render
{
    public class ViewParseContext
    {
        public IViewProvider ViewProvider { get; set; }

        public IJsBuilder Js { get; set; }

        public Document Dom { get; set; }
    }
}

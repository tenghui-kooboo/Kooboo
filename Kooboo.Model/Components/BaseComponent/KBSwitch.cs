using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.CtrlType
{
    public class KBSwitch : IComponent
    {
        public ComponentType Type => ComponentType.Switch;

        public VueField GetField()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

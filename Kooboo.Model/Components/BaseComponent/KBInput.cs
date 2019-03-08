using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components
{
    public class KBInput : IComponent
    {
        public ComponentType Type =>ComponentType.Input;

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

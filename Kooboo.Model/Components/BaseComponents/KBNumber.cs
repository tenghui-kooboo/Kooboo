using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.CtrlType
{
    public class KBNumber : BaseComponent
    {
        public override ComponentType Type => ComponentType.Number;

        public override VueField GetField()
        {
            return base.GetField();
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

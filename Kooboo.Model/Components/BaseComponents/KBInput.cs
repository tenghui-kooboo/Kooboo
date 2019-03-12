using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components
{
    public class KBInput : BaseComponent
    {
        public override ComponentType Type =>ComponentType.Input;

        public override VueField GetField()
        {
            var field = base.GetField();

            return field;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

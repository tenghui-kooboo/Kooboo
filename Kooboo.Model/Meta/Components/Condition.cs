using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class Condition : Dictionary<object, string>
    {
        public static Condition Boolean(string trueName, string falseName)
        {
            return new Condition
            {
                { true, trueName },
                { false, falseName }
            };
        }
    }
}

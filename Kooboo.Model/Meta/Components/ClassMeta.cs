using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class Class : List<object>
    {
        public Class()
        {
        }

        public Class(string classNames)
        {
            Add(classNames);
        }

        public Class AddCondition(Condition condition)
        {
            Add(condition);
            return this;
        }

        public static Class UseCondition(Condition condition)
        {
            return new Class().AddCondition(condition);
        }

        public static implicit operator Class(string classNames)
        {
            return new Class(classNames);
        }
    }
}

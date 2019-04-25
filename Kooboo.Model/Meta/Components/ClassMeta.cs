using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class ClassMeta : List<object>
    {
        public ClassMeta()
        {
        }

        public ClassMeta(string classNames)
        {
            Add(classNames);
        }

        public ClassMeta AddCondition(Condition condition)
        {
            Add(condition);
            return this;
        }

        public static ClassMeta UseCondition(Condition condition)
        {
            return new ClassMeta().AddCondition(condition);
        }

        public static implicit operator ClassMeta(string classNames)
        {
            return new ClassMeta(classNames);
        }
    }
}

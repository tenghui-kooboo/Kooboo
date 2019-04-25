using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class Localizable
    {
        public string Value { get; set; }

        public static implicit operator string(Localizable o)
        {
            return o.Value;
        }

        public static implicit operator Localizable(string o)
        {
            return new Localizable { Value = o };
        }
    }
}

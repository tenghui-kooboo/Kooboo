using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class Localizable
    {
        public Localizable(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}

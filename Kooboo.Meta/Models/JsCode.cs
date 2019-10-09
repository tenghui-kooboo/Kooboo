using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Models
{
    public struct JsCode
    {
        readonly string _code;

        public JsCode(string code)
        {
            _code = code;
        }

        public static implicit operator JsCode(string code)
        {
            if (code == null) return null;
            return new JsCode(code);
        }

        public override string ToString()
        {
            return _code;
        }
    }
}

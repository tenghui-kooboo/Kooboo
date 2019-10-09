using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Models
{
    public struct JsString
    {
        readonly string _code;

        public JsString(string code)
        {
            _code = code;
        }

        public static implicit operator JsString(string code)
        {
            if (code == null) return null;
            return new JsString(code);
        }

        public override string ToString()
        {
            return _code;
        }
    }
}

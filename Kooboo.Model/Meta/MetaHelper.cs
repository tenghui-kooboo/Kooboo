using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta
{
    public class MetaHelper
    {
        public static string ToCamalCaseName(string text)
        {
            return char.ToLower(text[0]) + text.Substring(1);
        }
    }
}

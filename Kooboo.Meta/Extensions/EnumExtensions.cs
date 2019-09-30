using Kooboo.Meta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class EnumExtensions
    {
        public static string ToName(this Enum @enum,string id)
        {
            return $"{@enum.ToString()}_{id}";
        }
    }
}

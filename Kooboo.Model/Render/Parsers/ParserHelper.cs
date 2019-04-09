using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Render.Parsers
{
    public class ParserHelper
    {
        public static string GetModelNameFromUrl(string url)
        {
            return url.Replace("/", "_");
        }

        public static string GetDefaultValueFromType(Type type)
        {
            if (typeof(IEnumerable<>).IsAssignableFrom(type))
                return "[]";

            return "null";
        }

        public static string GenerateUrlFromApiParameters(string url, string[] paramNames)
        {
            if (paramNames == null || !paramNames.Any())
                return url;

            var query = String.Join("&", paramNames.Select(o => $"{o}={{{o}}}"));
            return $"{url}?{query}";
        }
    }
}

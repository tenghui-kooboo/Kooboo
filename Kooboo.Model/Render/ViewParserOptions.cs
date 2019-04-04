using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Api;

namespace Kooboo.Model.Render
{
    public class ViewParseOptions
    {
        public string AttributePrefix { get; set; } = "k-";

        public IApiProvider ApiProvider { get; set; }
        /// <summary>
        /// Map from attribute name to corresponding renderer.
        /// </summary>
        public Dictionary<string, IVirtualElementParser> ElementParsers { get; } = new Dictionary<string, IVirtualElementParser>();

        public string GetVirtualElementName(string prefixedName)
        {
            return prefixedName.Substring(AttributePrefix.Length);
        }

        public string GetAttributeName(string unprefixName)
        {
            return AttributePrefix + unprefixName;
        }
    }
}

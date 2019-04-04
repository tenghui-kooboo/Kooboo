using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kooboo.Dom;

namespace Kooboo.Model.Render
{
    /// <summary>
    /// Virtual element who need special render
    /// </summary>
    public interface IVirtualElementParser
    {
        /// <summary>
        /// Attribute local name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Decide how to build JS
        /// </summary>
        void Parse(Element el, IJsBuilder js, ViewParseOptions options, Action visitChildren);
    }
}

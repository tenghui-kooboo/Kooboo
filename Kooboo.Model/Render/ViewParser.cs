using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kooboo.Dom;

namespace Kooboo.Model.Render
{ 
    public class ViewParser
    {
        public ViewParser(ViewParseOptions options)
        {
            Options = options;
        }

        public ViewParseOptions Options { get; }

        public void Parse(Document dom, IJsBuilder js)
        {
            VisitNode(dom, js);
        }

        private void VisitNode(Node node, IJsBuilder js)
        {
            if (!TryRenderNode(node, js, VisitChildern))
            {
                VisitChildern();
            }

            void VisitChildern()
            {
                foreach (var child in node.childNodes.item)
                {
                    VisitNode(child, js);
                }
            }
        }

        private bool TryRenderNode(Node node, IJsBuilder js, Action visitChildren)
        {
            var el = node as Element;
            if (el == null)
                return false;

            foreach (var attr in el.attributes)
            {
                // Attribute not start with prefix
                if (!attr.name.StartsWith(Options.AttributePrefix))
                    continue;

                // Not found handler for the attribute
                if (!Options.ElementParsers.TryGetValue(Options.GetVirtualElementName(attr.name), out IVirtualElementParser elRenderer))
                    continue;

                elRenderer.Parse(el, js, Options, visitChildren);
            }

            return false;
        }
    }

    public static class ViewRendererExtensions
    {
        public static void Parse(this ViewParser renderer, string html, IJsBuilder js)
        {
            renderer.Parse(DomParser.CreateDom(html), js);
        }
    }
}

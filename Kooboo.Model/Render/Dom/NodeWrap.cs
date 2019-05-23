using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.Dom;

namespace Kooboo.Model.Render
{
    public partial class NodeWrap
    {
        private Node _inner;

        public NodeWrap(DocumentWrap doc)
        {
            Document = doc;
        }

        public NodeWrap(DocumentWrap doc, Node node)
            : this(doc)
        {
            _inner = node;
        }

        public DocumentWrap Document { get; }

        public virtual Node Node => _inner;

        public IEnumerable<NodeWrap> Children
        {
            get
            {
                return _inner.childNodes.item.Select(o => Wrap(Document, o));
            }
        }

        public int StartIndex => Node.location.openTokenStartIndex;

        public int EndIndex => Node.location.endTokenEndIndex;

        internal virtual int Output(StringBuilder sb)
        {
            sb.Append(Node.OuterHtml);
            return EndIndex;
        }
    }

    partial class NodeWrap
    {
        private static Dictionary<enumNodeType, Type> WrapTypes = new Dictionary<enumNodeType, Type>
        {
            { enumNodeType.COMMENT, typeof(CommentWrap) },
            { enumNodeType.TEXT, typeof(TextWrap) },
            { enumNodeType.ELEMENT, typeof(ElementWrap) },
        };

        public static NodeWrap Wrap(DocumentWrap doc, Node node)
        {
            if (node.nodeType == enumNodeType.DOCUMENT)
                throw new ArgumentException("Do not create wrap for document by this method");

            if (WrapTypes.TryGetValue(node.nodeType, out var wrapType))
            {
                return Activator.CreateInstance(wrapType, node) as NodeWrap;
            }
            else
            {
                return new NodeWrap(doc, node);
            }
        }
    }
}

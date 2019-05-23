using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.Dom;

namespace Kooboo.Model.Render
{
    // Todo: Add action monitor is not implemented yet
    public class DocumentWrap
    {
        private Document _inner;
        private SortedDictionary<int, ChangeEntry> _modified = new SortedDictionary<int, ChangeEntry>();

        public DocumentWrap(Document doc)
        {
            _inner = doc;
        }

        public IEnumerable<NodeWrap> Children
        {
            get
            {
                return _inner.childNodes.item.Select(o => NodeWrap.Wrap(this, o));
            }
        }

        internal void NotifyModified(NodeWrap node)
        {
            if (!_modified.TryGetValue(node.StartIndex, out var exist))
            {
                _modified[node.StartIndex] = new ChangeEntry { Type = ChangeType.Modified, Node = node };
            }
        }

        internal void NotifyRemoved(NodeWrap node)
        {
            if (!_modified.TryGetValue(node.StartIndex, out var entry))
            {
                _modified[node.StartIndex] = new ChangeEntry { Type = ChangeType.Removed, Node = node };
            }
            else
            {
                if (entry.Type == ChangeType.Added)
                {
                    _modified.Remove(node.StartIndex);
                }
                else if (entry.Type == ChangeType.Modified)
                {
                    entry.Type = ChangeType.Removed;
                }
            }
        }

        public override string ToString()
        {
            var source = _inner.HtmlSource;
            var sb = new StringBuilder();
            var p = 0;

            foreach (var each in _modified)
            {
                if (p < each.Key)
                {
                    sb.Append(source.Substring(p, each.Key - p));
                }

                p = each.Value.Node.Output(sb);
            }

            if (p < source.Length - 1)
            {
                sb.Append(source.Substring(p));
            }

            return sb.ToString();
        }

        class ChangeEntry
        {
            public ChangeType Type { get; set; }

            public NodeWrap Node { get; set; }
        }

        enum ChangeType
        {
            Added,

            Modified,

            Removed
        }
    }
}

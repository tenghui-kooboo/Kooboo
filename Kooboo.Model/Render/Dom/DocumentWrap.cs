using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.Dom;

namespace Kooboo.Model.Render
{
    // Todo: Add action monitor is not implemented yet
    public class DocumentWrap : NodeWrap
    {
        private Document _inner;
        private SortedDictionary<int, ChangeEntry> _modified = new SortedDictionary<int, ChangeEntry>();

        public DocumentWrap(Document doc)
            : base(null)
        {
            _inner = doc;
        }

        public DocumentWrap(string html)
            : this(Dom.DomParser.CreateDom(html))
        {
        }

        /// <summary>
        /// For unit test
        /// </summary>
        public SortedDictionary<int, ChangeEntry> Modified => _modified;

        public override DocumentWrap Document => this;

        public override Node Node => _inner;

        public ElementWrap Body => new ElementWrap(this, _inner.body);

        public IEnumerable<ElementWrap> Select(string cssSelector)
        {
            return _inner.Select(cssSelector).item.Select(o => new ElementWrap(this, o));
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

                if (each.Value.Type == ChangeType.Modified)
                {
                    p = each.Value.Node.Output(sb);
                }
                else if (each.Value.Type == ChangeType.Removed)
                {
                    p = each.Value.Node.EndIndex + 1;
                }
                else
                {
                    throw new NotSupportedException("Add element not supported yet");
                }
            }

            if (p < source.Length - 1)
            {
                sb.Append(source.Substring(p));
            }

            return sb.ToString();
        }

        public void NotifyModified(NodeWrap node)
        {
            if (!_modified.TryGetValue(node.StartIndex, out var exist))
            {
                _modified[node.StartIndex] = new ChangeEntry { Type = ChangeType.Modified, Node = node };
            }
        }

        public void NotifyRemoved(NodeWrap node)
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


        public class ChangeEntry
        {
            public ChangeType Type { get; set; }

            public NodeWrap Node { get; set; }
        }

        public enum ChangeType
        {
            Added,

            Modified,

            Removed
        }
    }

    public static class DocumentWrapExtensions
    {
        public static ElementWrap RootWithoutBody(this DocumentWrap doc)
        {
            return doc.Body.Children.First() as ElementWrap;
        }
    }
}

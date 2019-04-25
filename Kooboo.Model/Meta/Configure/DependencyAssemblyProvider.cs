using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kooboo.Model.Meta.Configure
{
    public class DependencyAssemblyProvider : IAssemblyProvider
    {
        private IEnumerable<Assembly> _orignal;
        private IEnumerable<Assembly> _ordered;
        private object _lock = new object();

        public DependencyAssemblyProvider(IEnumerable<Assembly> assemblies)
        {
            _orignal = assemblies;
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            if (_ordered != null)
                return _ordered;

            lock (_lock)
            {
                if (_ordered != null)
                    return _ordered;

                _ordered = OrderByDependency(_orignal);
                return _ordered;
            }
        }

        private IEnumerable<Assembly> OrderByDependency(IEnumerable<Assembly> original)
        {
            var tree = original.ToDictionary(o => o.GetName().Name, o => new Node { Assembly = o });

            // Build relations
            foreach (var node in tree.Values)
            {
                foreach (var reference in node.Assembly.GetReferencedAssemblies())
                {
                    if (tree.TryGetValue(reference.Name, out Node refNode))
                    {
                        node.References.Add(refNode);
                        refNode.ReferencedBy.Add(node);
                    }
                }
            }

            // Remove and ordered from zero references
            var ordered = new List<Assembly>();
            foreach (var each in tree.Values.Where(o => o.References.Count == 0).ToArray())
            {
                RemoveNode(each);
            }

            return ordered;

            void RemoveNode(Node node)
            {
                tree.Remove(node.Assembly.GetName().Name);
                ordered.Add(node.Assembly);

                foreach (var each in node.ReferencedBy)
                {
                    each.References.Remove(node);

                    if (each.References.Count == 0)
                    {
                        RemoveNode(each);
                    }
                }
            }
        }

        class Node
        {
            public string Name { get; set; }

            public Assembly Assembly { get; set; }

            public List<Node> References { get; set; } = new List<Node>();

            public List<Node> ReferencedBy { get; set; } = new List<Node>();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class NamedMetaCollection<T> : IEnumerable<T>
        where T : INamedMeta
    {
        private List<T> _list = new List<T>();
        private Dictionary<string, T> _map = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);

        public T EnsureItem(string name)
        {
            if (_map.TryGetValue(name, out T item))
                return item;

            item = Activator.CreateInstance<T>();
            item.Name = name;
            _list.Add(item);
            _map[item.Name] = item;

            return item; 
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}

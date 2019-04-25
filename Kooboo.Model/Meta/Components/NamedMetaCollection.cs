using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public partial class NamedMetaCollection<T> : IEnumerable<T>
        where T : INamedMeta
    {
        private List<T> _list = new List<T>();
        private Dictionary<string, T> _map = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);

        public NamedMetaCollection()
        {
        }

        public NamedMetaCollection(IEnumerable<T> items)
        {
            _map = items.ToDictionary(o => o.Name, o => o);
            _list = items.ToList();
        }

        public int Count => _list.Count;

        public int FindIndex(string name) => _list.FindIndex(o => o.Name == name);

        public bool TryGet(string name, out T item) => _map.TryGetValue(name, out item);

        public bool TryAdd(string name, out T item)
        {
            if (!TryCreate(name, out item))
                return false;

            _list.Add(item);
            return true;
        }

        public bool TryInsert(int index, string name, out T item)
        {
            if (!TryCreate(name, out item))
                return false;

            _list.Insert(index, item);
            return true;
        }

        public bool TryCreate(string name, out T item)
        {
            if (_map.TryGetValue(name, out item))
                return false;

            item = Activator.CreateInstance<T>();
            item.Name = name;
            _map[item.Name] = item;

            return true;
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

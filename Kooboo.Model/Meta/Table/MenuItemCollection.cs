using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kooboo.Model.Meta.Table
{
    public class MenuItemCollection : IList<MenuItem>
    {
        private List<MenuItem> _inner;

        public MenuItemCollection()
        {
            _inner = new List<MenuItem>();
        }

        public MenuItem this[int index] { get => _inner[index]; set => _inner[index] = value; }

        public int Count => _inner.Count;

        bool ICollection<MenuItem>.IsReadOnly => (_inner as ICollection<MenuItem>).IsReadOnly;

        public void Add(MenuItem item)
        {
            _inner.Add(item);
            _inner = _inner.OrderBy(o => o.Order).OrderBy(o => o.Order).ToList();
        }

        public void Clear()
        {
            _inner.Clear();
        }

        public bool Contains(MenuItem item)
        {
            return _inner.Contains(item);
        }

        public void CopyTo(MenuItem[] array, int arrayIndex)
        {
            _inner.CopyTo(array, arrayIndex);
        }

        public int IndexOf(MenuItem item)
        {
            return _inner.IndexOf(item);
        }

        public void Insert(int index, MenuItem item)
        {
            _inner.Insert(index, item);
            _inner = _inner.OrderBy(o => o.Order).OrderBy(o => o.Order).ToList();
        }

        public bool Remove(MenuItem item)
        {
            return _inner.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _inner.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_inner as IEnumerable).GetEnumerator();
        }

        IEnumerator<MenuItem> IEnumerable<MenuItem>.GetEnumerator()
        {
            return (_inner as IEnumerable<MenuItem>).GetEnumerator();
        }
    }
}

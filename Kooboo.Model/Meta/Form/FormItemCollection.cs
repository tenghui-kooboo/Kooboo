using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Form
{
    public class FormItemCollection : IEnumerable<FormItem>
    {
        private List<FormItem> _list = new List<FormItem>();
        private Dictionary<string, FormItem> _map = new Dictionary<string, FormItem>(StringComparer.OrdinalIgnoreCase);

        public FormItem EnsureItem(string name)
        {
            if (_map.TryGetValue(name, out FormItem item))
                return item;

            item = new FormItem { Name = name };
            _list.Add(item);
            _map[item.Name] = item;

            return item; 
        }

        IEnumerator<FormItem> IEnumerable<FormItem>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}

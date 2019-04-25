using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public interface INamedMeta
    {
        string Name { get; set; }
    }

    public static class NamedMetaExtensions
    {
        public static T Find<T>(this List<T> list, string name)
            where T : INamedMeta
        {
            return list.Find(o => o.Name == name);
        }

        public static bool Before<T>(this List<T> list, string name, T item)
            where T : INamedMeta
        {
            var index = list.FindIndex(o => o.Name == name);
            if (index < 0)
                return false;

            list.Insert(index, item);
            return true;
        }

        public static bool After<T>(this List<T> list, string name, T item)
            where T : INamedMeta
        {
            var index = list.FindIndex(o => o.Name == name);
            if (index < 0)
                return false;

            if (index == list.Count - 1)
            {
                list.Add(item);
            }
            else
            {
                list.Insert(index + 1, item);
            }
            return true;
        }
    }
}

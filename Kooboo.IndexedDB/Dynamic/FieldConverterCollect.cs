using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.IndexedDB.Dynamic
{
    public class FieldConverterCollect : List<FieldConverter>
    {
        Dictionary<string, FieldConverter> fieldNameDic = new Dictionary<string, FieldConverter>();
        Dictionary<int, FieldConverter> fieldNameHashDic = new Dictionary<int, FieldConverter>();
        public new void Add(FieldConverter f)
        {
            fieldNameDic.Add(f.FieldName, f);
            fieldNameHashDic.Add(f.FieldNameHash, f);
            base.Add(f);
        }

        /// <summary>
        /// 获取NameHash
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public FieldConverter FindNameHash(int hash)
        {
            if (fieldNameHashDic.TryGetValue(hash, out var value))
                return value;
            return null;
        }

        /// <summary>
        /// 获取Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FieldConverter FindName(string name)
        {
            if (fieldNameDic.TryGetValue(name, out var value))
                return value;
            return null;
        }
    }
}

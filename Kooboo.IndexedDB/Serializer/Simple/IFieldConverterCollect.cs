using Kooboo.IndexedDB.Serializer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.IndexedDB.Serializer
{
    public class IFieldConverterCollect : List<IFieldConverter>
    {
        Dictionary<int, IFieldConverter> fieldNameHashDic = new Dictionary<int, IFieldConverter>();
        public new void Add(IFieldConverter f)
        {
            fieldNameHashDic.Add(f.FieldNameHash, f);
            base.Add(f);
        }

        /// <summary>
        /// 获取NameHash
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public IFieldConverter FindNameHash(int hash) => fieldNameHashDic[hash];
    }
    public class IFieldConverterCollect<T> : List<IFieldConverter<T>>
    {
        Dictionary<int, IFieldConverter<T>> fieldNameHashDic = new Dictionary<int, IFieldConverter<T>>();
        public new void Add(IFieldConverter<T> f)
        {
            fieldNameHashDic.Add(f.FieldNameHash, f);
            base.Add(f);
        }

        /// <summary>
        /// 获取NameHash
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public IFieldConverter<T> FindNameHash(int hash)
        {
            if (fieldNameHashDic.TryGetValue(hash, out var value))
                return value;
            return null;
        }
    }
}

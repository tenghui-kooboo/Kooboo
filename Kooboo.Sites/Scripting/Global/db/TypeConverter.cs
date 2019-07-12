using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Sites.Scripting.Global.Db
{
    public class TypeConverter
    {
        public Dictionary<string, string> dbTypes = new Dictionary<string, string>()
        {
            { typeof(bool).FullName,"bit(1)"},
            { typeof(sbyte).FullName,"tinyint(3)"},
            { typeof(short).FullName,"smallint(6)"},
            { typeof(int).FullName,"int(11)"},
            { typeof(long).FullName,"bigint(20)"},{ typeof(Int64).FullName,"bigint(20)"},
            { typeof(byte).FullName,"tinyint(3) unsigned"},
            { typeof(ushort).FullName,"smallint(5) unsigned"},
            { typeof(uint).FullName,"int(10) unsigned"},
            { typeof(ulong).FullName,"bigint(20) unsigned"},
            { typeof(double).FullName,"double"},
            { typeof(float).FullName,"float"},
            { typeof(decimal).FullName,"decimal(10,2)"},

            { typeof(TimeSpan).FullName,"time"},
            { typeof(DateTime).FullName,"datetime"},
            { typeof(byte[]).FullName,"varbinary(255)"},
            { typeof(string).FullName,"varchar(255)"},
            { typeof(Guid).FullName,"char(36)"},
        };

        public string GetDataType(Type type)
        {
            if(dbTypes.TryGetValue(type.FullName,out var dbtype))
            {
                return dbtype;
            }

            return null;
        }
    }
}

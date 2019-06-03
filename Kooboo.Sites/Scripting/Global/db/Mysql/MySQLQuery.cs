using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kooboo.Sites.Scripting.Global.Db
{
    public class MySQLQuery : TableQuery
    {
        public MySQLQuery(IkTable table):base(table)
        {
        }
        
        public override DynamicTableObject[] take(int count)
        {
            var connection = DataBus.GetDataBase(ktable.TableContext.ConnectionString);
            var sql = SQLHelper.GetSelectSql(ktable.TableContext.TableName, SearchCondition, OrderByField, Ascending, skipcount, count);

            var list = connection.ExecuteList<object>(sql, null).ToArray();
            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(i as IDictionary<string, object>, null, null)).ToArray();

            return objects;
        }

        public override int count()
        {
            var connection = DataBus.GetDataBase(ktable.TableContext.ConnectionString);
            var sql = SQLHelper.GetCountSql(ktable.TableContext.TableName, SearchCondition);

            var data = connection.ExecuteSingle<object>(sql, null);

            var dic = data as IDictionary<string, object>;
            object obj = null;
            if (dic != null && dic.TryGetValue("count", out obj))
            {
                int count = 0;
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
                return count;

            }

            return 0;
        }
        
    }
}

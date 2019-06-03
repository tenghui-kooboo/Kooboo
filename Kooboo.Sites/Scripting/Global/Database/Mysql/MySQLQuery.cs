using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kooboo.Sites.Scripting.Global.Database
{
    public class MySQLQuery : ITableQuery
    {
        private MySQLTable table;
        public MySQLQuery(MySQLTable table)
        {
            this.table = table;
        }
        public int skipcount { get; set; }

        public bool Ascending { get; set; }

        public string OrderByField { get; set; }

        public string SearchCondition { get; set; }

        public ITableQuery OrderBy(string fieldname)
        {
            this.OrderByField = fieldname;
            this.Ascending = true;
            return this;
        }

        public ITableQuery OrderByDescending(string fieldname)
        {
            this.OrderByField = fieldname;
            this.Ascending = false;
            return this;
        }

        public ITableQuery skip(int skip)
        {
            this.skipcount = skip;
            return this;
        }

        public ITableQuery Where(string searchCondition)
        {
            this.SearchCondition = searchCondition;
            return this;
        }

        public DynamicTableObject[] take(int count)
        {
            var connection = DataBus.GetDataBase(table.Database.connectionString);
            var sql = SQLHelper.GetSelectSql(table.Name, SearchCondition, OrderByField, Ascending, skipcount, count);

            var list = connection.ExecuteList<object>(sql, null).ToArray();
            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(i as IDictionary<string, object>, null, null)).ToArray();

            return objects;
        }

        public int count()
        {
            var connection = DataBus.GetDataBase(table.Database.connectionString);
            var sql = SQLHelper.GetCountSql(table.Name, SearchCondition);

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

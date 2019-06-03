using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kooboo.Sites.Scripting.Global.Db.IndexDb
{
    public class IndexDbQuery:TableQuery
    {
        public IndexDbQuery(IkTable table) : base(table)
        {

        }

        public override DynamicTableObject[] take(int count)
        {
            var query = new IndexedDB.Dynamic.Query(this.ktable.TableContext.Table);

            if (!string.IsNullOrEmpty(this.SearchCondition))
            {
                var filter = query.ParserFilter(this.SearchCondition);
                query.items = filter;
            }
            else
            {
                //throw new Exception("You do not any search condition");
            }

            if (!string.IsNullOrEmpty(this.OrderByField))
            {
                if (this.Ascending)
                {
                    query.OrderByAscending(this.OrderByField);
                }
                else
                {
                    query.OrderByDescending(this.OrderByField);
                }
            }

            var result = query.Skip(this.skipcount).Take(count).ToArray();
            return DynamicTableObject.CreateList(result, this.ktable.TableContext.Table, this.ktable.context);

        }

        public override int count()
        {
            // TODO: improve performance.
            var all = take(99999);
            if (all == null)
            {
                return 0;
            }
            else
            {
                return all.Count();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Sites.Scripting.Global.Db
{
    public interface ITableQuery
    {
        int skipcount { get; set; }

        bool Ascending { get; set; }

        string OrderByField { get; set; }

        string SearchCondition { get; set; }

        ITableQuery skip(int skip);

        ITableQuery Where(string searchCondition);

        ITableQuery OrderBy(string fieldname);

        ITableQuery OrderByDescending(string fieldname);

        DynamicTableObject[] take(int count);

        int count();

    }
}

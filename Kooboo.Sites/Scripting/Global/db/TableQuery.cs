//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Sites.Scripting.Global.Db;

namespace Kooboo.Sites.Scripting.Global
{
    public class TableQuery
    {
        public TableQuery(IkTable table)
        {
            this.ktable = table;  
        }

        [Attributes.SummaryIgnore]
        public IkTable ktable { get; set; }

        public int skipcount { get; set; }

        public bool Ascending { get; set; }

        public string OrderByField { get; set; }

        public string SearchCondition { get; set; }

        public TableQuery skip(int skip)
        { 
            this.skipcount = skip;
            return this;
        }

        public TableQuery Where(string searchCondition)
        {
            this.SearchCondition = searchCondition;
            return this;
        }

        public TableQuery OrderBy(string fieldname)
        {
            this.OrderByField = fieldname;
            this.Ascending = true;
            return this;
        }

        public TableQuery OrderByDescending(string fieldname)
        {
            this.OrderByField = fieldname;
            this.Ascending = false;
            return this;
        }

        public virtual DynamicTableObject[] take(int count)
        {
            return null;
        }
         
        public virtual int count()
        {
            return 0;
        }
    }
}

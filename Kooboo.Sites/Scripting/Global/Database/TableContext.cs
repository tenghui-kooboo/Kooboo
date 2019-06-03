using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.IndexedDB;
using Kooboo.IndexedDB.Dynamic;
using Kooboo.Data.Context;

namespace Kooboo.Sites.Scripting.Global.Database
{
    public class TableContext
    {
        public string ConnectionString { get; set; }

        public string TableName { get; set; }

        public Table Table { get; set; }

        public RenderContext RenderContext { get; set; }
    }
}

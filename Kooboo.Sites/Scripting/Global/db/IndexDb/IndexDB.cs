using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Context;
using Kooboo.Data.Interface;

namespace Kooboo.Sites.Scripting.Global.Db
{
    public class IndexDB:Database
    {
        public IndexDB(RenderContext context) : base(context)
        {

        }
        public override string Name => "indexdb";

        public override IkTable GetTable(string name)
        {
            var db = Kooboo.Data.DB.GetKDatabase(this.context.WebSite);
            var tb = db.GetOrCreateTable(name);
            return new KTable(tb, this.context);
        }

        public override void ExecuteSP(string procName, IDictionary<string, object> parameters)
        {
            throw new NotSupportedException();
        }

        public override DynamicTableObject[] QueryBySP(string proceName, IDictionary<string, object> parameters)
        {
            throw new NotSupportedException();
        }

        public override DynamicTableObject QueryFirstBySP(string proceName, IDictionary<string, object> parameters)
        {
            throw new NotSupportedException();
        }

        public override DynamicTableObject QueryFirst(string sql, IDictionary<string, object> parameters)
        {
            throw new NotSupportedException();
        }

        public override DynamicTableObject[] Query(string sql, IDictionary<string, object> parameters)
        {
            throw new NotSupportedException();
        }

        public override void ExecuteNoQuery(string sql, IDictionary<string, object> parameters)
        {
            throw new NotSupportedException();
        }
    }
}

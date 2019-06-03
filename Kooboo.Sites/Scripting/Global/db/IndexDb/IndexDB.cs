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
            var tb = db.GetOrCreateTable(Name);
            return new KTable(tb, this.context);
        }
    }
}

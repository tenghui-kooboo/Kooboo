using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Context;
using Kooboo.Data.Interface;
using System.Linq;

namespace Kooboo.Sites.Scripting.Global.Db
{

    public class MySQLSetting : ISiteSetting
    {
        public string Name => "MySQL";

        public string ConnectionString { get; set; }
    }

    public class KMySQL : Database
    {
        public override string Name => "mysql";

        public KMySQL(RenderContext context) : base(context)
        {
            var setting = GetSetting<MySQLSetting>(context);
            if (setting != null)
            {
                connectionString = setting.ConnectionString;
            }
            //if(string.IsNullOrEmpty(connectionString))
            //{
            //    throw new Exception("Please set your connection string");
            //}
        }

        public override IkTable GetTable(string name)
        {
            var tableContext = new TableContext()
            {
                ConnectionString = this.connectionString,
                TableName = name,
                RenderContext=context
            };
            var table = new MySQLTable()
            {
                TableContext= tableContext,
                context =context
            };
            return table;
        }
    }

}

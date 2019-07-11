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

        #region store procedure
        public override void ExecuteSP(string procName, IDictionary<string, object> parameters)
        {
            var connection = DataBus.GetDataBase(connectionString);
            connection.ExecuteSP(procName, parameters);
        }

        public override DynamicTableObject[] QueryBySP(string procName, IDictionary<string, object> parameters)
        {
            var connection = DataBus.GetDataBase(connectionString);
            var list = connection.QueryBySP<object>(procName, parameters);

            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(i as IDictionary<string, object>, null, this.context)).ToArray();

            return objects;
        }

        public override DynamicTableObject QueryFirstBySP(string procName, IDictionary<string, object> parameters)
        {
            var connection = DataBus.GetDataBase(connectionString);

            var data = connection.QueryFirstBySP<object>(procName, parameters);

            return  DynamicTableObject.Create(data as IDictionary<string,object>, null, this.context);
        }


        #endregion

        #region
        public override DynamicTableObject QueryFirst(string sql, IDictionary<string, object> parameters)
        {
            var connection = DataBus.GetDataBase(connectionString);
            var data = connection.ExecuteSingle<object>(sql, parameters);

            return DynamicTableObject.Create(data as IDictionary<string, object>, null, this.context);
        }

        public override DynamicTableObject[] Query(string sql, IDictionary<string, object> parameters)
        {
            var connection = DataBus.GetDataBase(connectionString);
            var list = connection.ExecuteList<object>(sql, parameters);

            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(i as IDictionary<string, object>, null, this.context)).ToArray();

            return objects;
        }

        public override void ExecuteNoQuery(string sql, IDictionary<string, object> parameters)
        {
            var connection = DataBus.GetDataBase(connectionString);
            connection.ExecuteNoQuery(sql, parameters);
        }
        #endregion
    }

}

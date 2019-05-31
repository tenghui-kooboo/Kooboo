using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Context;
using Kooboo.Data.Interface;

namespace Kooboo.Sites.Scripting.Global.Database
{

    public class MySQLSetting : ISiteSetting
    {
        public string Name => "MySQL";

        public string ConnectionString { get; set; }
    }

    public class KMySQL : Database
    {
        protected override string Name => "mysql";

        public KMySQL(RenderContext context) : base(context)
        {
            var setting = GetSetting<MySQLSetting>(context);
            if (setting != null)
            {
                connectionString = setting.ConnectionString;
            }
            if(string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Please set your connection string");
            }
        }

        public override IkTable GetTable(string name)
        {
            var table = new MySQLTable()
            {
                Database = this,
                Name = name,
                context=context
            };
            return table;
        }
    }

    public class MySQLTable : IkTable
    {
        public Database Database { get; set; }
        public string Name { get; set; }
        public RenderContext context { get; set; }
        #region old method
        public object add(object value)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);

            if(value is IDictionary<string, object>)
            {
                var dic=value as IDictionary<string,object>;
                if (dic.Count > 0)
                {
                    string sql = SQLHelper.GetInsertSql(Name, dic);
                    connection.ExecuteNoQuery(sql, dic);
                    return true;
                }
                
            }

            return false;
        }

        public object append(object value)
        {
            throw new NotImplementedException();
        }

        public void delete(object id)
        {
            throw new NotImplementedException();
        }

        public object find(string searchCondition)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);
            var sql = SQLHelper.GetSelectSql(Name, searchCondition);

            var data= connection.ExecuteSingle<object>(sql, null);
            return data;
        }

        public object[] findAll(string condition)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);
            var sql = SQLHelper.GetSelectSql(Name,condition);
            return connection.ExecuteList<object>(sql, null).ToArray();
        }

        public object get(object id)
        {
            throw new NotImplementedException();
        }

        public void update(object id, object newvalue)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);

            if (newvalue is IDictionary<string, object>)
            {
                var dic = newvalue as IDictionary<string, object>;
                if (dic.Count > 0)
                {
                    string sql = SQLHelper.GetUpdateSql(Name, dic,id.ToString());
                    connection.ExecuteNoQuery(sql, dic);
                }

            }
        }

        public void update(object newvalue)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region new method

        public object get(string key,object id)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);
            var sql = SQLHelper.GetSelectSql(Name, string.Format("{0}=@{0}", key));
            var dic = new Dictionary<string, object>();
            dic.Add(key, id);
            return connection.ExecuteSingle<object>(sql, dic);
            
        }
        public void delete(string key, object value)
        {
            var dic = new Dictionary<string, object>();
            dic.Add(key, value);

            var connection = DataBus.GetDataBase(Database.connectionString);
            string sql = SQLHelper.GetDeleteSql(Name, key);
            connection.ExecuteNoQuery(sql, dic);
        }

        #endregion
    }


    public class SQLHelper
    {
        public static string GetInsertSql(string tableName,IDictionary<string,object> dic)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("insert into {0}", tableName);

            var fieldBuilder = new StringBuilder();
            var valueBuilder = new StringBuilder();
            foreach(var keypair in dic)
            {
                if (fieldBuilder.Length > 0)
                    fieldBuilder.Append(",");
                fieldBuilder.AppendFormat("{0}", keypair.Key);

                if (valueBuilder.Length > 0)
                    valueBuilder.Append(",");
                valueBuilder.AppendFormat("@{0}", keypair.Key);
            }

            builder.AppendFormat("({0})",fieldBuilder.ToString());
            builder.AppendFormat("values({0})", valueBuilder.ToString());

            return builder.ToString();
        }

        public static string GetUpdateSql(string tableName, IDictionary<string, object> dic,string primaryKey)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("update {0} set ", tableName);

            var updateBuilder = new StringBuilder();
            foreach (var keypair in dic)
            {
                if (updateBuilder.Length > 0)
                    updateBuilder.Append(",");
                updateBuilder.AppendFormat("{0}=@{0}", keypair.Key);
            }

            builder.Append(updateBuilder.ToString());
            builder.AppendFormat(" where {0}=@{0}", primaryKey);

            return builder.ToString();
        }

        public static string GetDeleteSql(string tableName,string primaryKey)
        {
            return string.Format("delete from {0} where {1}=@{1}", tableName, primaryKey);
        }

        public static string GetSelectSql(string tableName,string condition)
        {
            return string.Format("select * from {0} where {1}",tableName ,condition);
        }

    }
}

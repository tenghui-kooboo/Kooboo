using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Context;
using Kooboo.Data.Interface;
using System.Linq;

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

        //unused
        public object append(object value)
        {
            throw new NotImplementedException();
        }
        //can't use this method
        public void delete(object id)
        {
            throw new NotImplementedException();
        }

        public DynamicTableObject find(string searchCondition)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);
            var sql = SQLHelper.GetSelectSql(Name, searchCondition);

            var data= connection.ExecuteSingle<object>(sql, null);
            var dic = Convert(data);
            
            return DynamicTableObject.Create(dic, null, this.context);
        }

        public DynamicTableObject find(string field, object value)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);

            var searchCondition = string.Format("{0}=@{0}", field);
            var sql = SQLHelper.GetSelectSql(Name, searchCondition);

            var param = new Dictionary<string, object>();
            param.Add(field, value);
            var data = connection.ExecuteSingle<object>(sql, param);

            return DynamicTableObject.Create(Convert(data), null, this.context);
        }

        public DynamicTableObject[] findAll(string field, object value)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);

            var condition = string.Format("{0}=@{0}", field);
            var sql = SQLHelper.GetSelectSql(Name, condition);

            var param = new Dictionary<string, object>();
            param.Add(field, value);
            var list = connection.ExecuteList<object>(sql, param).ToArray();

            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(Convert(i), null, this.context)).ToArray();

            return objects;
        }

        public DynamicTableObject[] findAll(string condition)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);
            var sql = SQLHelper.GetSelectSql(Name,condition);
            
            var list= connection.ExecuteList<object>(sql, null).ToArray();
            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(Convert(i),null,this.context)).ToArray();

            return objects;
        }

        //can't use this method
        public DynamicTableObject get(object id)
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
        //can't use this method
        public void update(object newvalue)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region new method

        public DynamicTableObject get(string key,object id)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);
            var sql = SQLHelper.GetSelectSql(Name, string.Format("{0}=@{0}", key));
            var dic = new Dictionary<string, object>();
            dic.Add(key, id);
            var data= connection.ExecuteSingle<object>(sql, dic);

            return DynamicTableObject.Create(Convert(data), null, this.context);
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

        #region private
        private IDictionary<string,object> Convert(object data)
        {
            return data as IDictionary<string, object>;
        }

        public ITableQuery Query()
        {
            return new MySQLQuery(this);
        }

        public ITableQuery Query(string searchCondition)
        {
            var result = new MySQLQuery(this);
            result.Where(searchCondition);
            return result;
        }

        public void createIndex(string fieldname)
        {
            var connection = DataBus.GetDataBase(Database.connectionString);
            var sql = SQLHelper.CreateIndexSql(Name,fieldname);
            connection.ExecuteNoQuery(sql, null);
        }

        public DynamicTableObject[] all()
        {
            var connection = DataBus.GetDataBase(Database.connectionString);
            var sql = SQLHelper.GetSelectSql(Name,string.Empty);

            var list = connection.ExecuteList<object>(sql, null).ToArray();
            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(Convert(i), null, this.context)).ToArray();

            return objects;
        }
        #endregion
    }

}

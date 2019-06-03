using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Context;
using System.Linq;

namespace Kooboo.Sites.Scripting.Global.Db
{
    public class MySQLTable:IkTable
    {
        public TableContext TableContext { get; set; }

        public RenderContext context { get; set; }

        public object add(object value)
        {
            var connection = DataBus.GetDataBase(TableContext.ConnectionString);

            if (value is IDictionary<string, object>)
            {
                var dic = value as IDictionary<string, object>;
                if (dic.Count > 0)
                {
                    string sql = SQLHelper.GetInsertSql(TableContext.TableName, dic);
                    return connection.ExecuteScalar(sql, dic);
                }
            }

            return null;
        }

        public DynamicTableObject find(string searchCondition)
        {
            var connection = DataBus.GetDataBase(TableContext.ConnectionString);
            var sql = SQLHelper.GetSelectSql(TableContext.TableName, searchCondition);

            var data = connection.ExecuteSingle<object>(sql, null);
            var dic = Convert(data);

            return DynamicTableObject.Create(dic, null, this.context);
        }

        public DynamicTableObject find(string field, object value)
        {
            var connection = DataBus.GetDataBase(TableContext.ConnectionString);

            var searchCondition = string.Format("{0}=@{0}", field);
            var sql = SQLHelper.GetSelectSql(TableContext.TableName, searchCondition);

            var param = new Dictionary<string, object>();
            param.Add(field, value);
            var data = connection.ExecuteSingle<object>(sql, param);

            return DynamicTableObject.Create(Convert(data), null, this.context);
        }

        public DynamicTableObject[] findAll(string field, object value)
        {
            var connection = DataBus.GetDataBase(TableContext.ConnectionString);

            var condition = string.Format("{0}=@{0}", field);
            var sql = SQLHelper.GetSelectSql(TableContext.TableName, condition);

            var param = new Dictionary<string, object>();
            param.Add(field, value);
            var list = connection.ExecuteList<object>(sql, param).ToArray();

            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(Convert(i), null, this.context)).ToArray();

            return objects;
        }

        public DynamicTableObject[] findAll(string condition)
        {
            var connection = DataBus.GetDataBase(TableContext.ConnectionString);
            var sql = SQLHelper.GetSelectSql(TableContext.TableName, condition);

            var list = connection.ExecuteList<object>(sql, null).ToArray();
            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(Convert(i), null, this.context)).ToArray();

            return objects;
        }

        public void update(object id, object newvalue)
        {
            var connection = DataBus.GetDataBase(TableContext.ConnectionString);

            if (newvalue is IDictionary<string, object>)
            {
                var dic = newvalue as IDictionary<string, object>;
                if (dic.Count > 0)
                {
                    string sql = SQLHelper.GetUpdateSql(TableContext.TableName, dic, id.ToString());
                    connection.ExecuteNoQuery(sql, dic);
                }

            }
        }

        public TableQuery Query()
        {
            return new MySQLQuery(this);
        }

        public TableQuery Query(string searchCondition)
        {
            var result = new MySQLQuery(this);
            result.Where(searchCondition);
            return result;
        }

        public void createIndex(string fieldname)
        {
            var connection = DataBus.GetDataBase(TableContext.ConnectionString);
            var sql = SQLHelper.CreateIndexSql(TableContext.TableName, fieldname);
            connection.ExecuteNoQuery(sql, null);
        }

        public DynamicTableObject[] all()
        {
            var connection = DataBus.GetDataBase(TableContext.ConnectionString);
            var sql = SQLHelper.GetSelectSql(TableContext.TableName, string.Empty);

            var list = connection.ExecuteList<object>(sql, null).ToArray();
            DynamicTableObject[] objects = list.Select(i => DynamicTableObject.Create(Convert(i), null, this.context)).ToArray();

            return objects;
        }

        #region new method

        public DynamicTableObject get(string key, object id)
        {
            var connection = DataBus.GetDataBase(TableContext.ConnectionString);
            var sql = SQLHelper.GetSelectSql(TableContext.TableName, string.Format("{0}=@{0}", key));
            var dic = new Dictionary<string, object>();
            dic.Add(key, id);
            var data = connection.ExecuteSingle<object>(sql, dic);

            return DynamicTableObject.Create(Convert(data), null, this.context);
        }
        public void delete(string key, object value)
        {
            var dic = new Dictionary<string, object>();
            dic.Add(key, value);

            var connection = DataBus.GetDataBase(TableContext.ConnectionString);
            string sql = SQLHelper.GetDeleteSql(TableContext.TableName, key);
            connection.ExecuteNoQuery(sql, dic);
        }


        #endregion

        #region NotImplemented
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

        //can't use this method
        public DynamicTableObject get(object id)
        {
            throw new NotImplementedException();
        }
        //can't use this method
        public void update(object newvalue)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region private
        private IDictionary<string, object> Convert(object data)
        {
            return data as IDictionary<string, object>;
        }

        #endregion
    }
}

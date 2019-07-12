using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kooboo.Sites.Scripting.Global.Db
{
    public class SchemaHelper
    {
        public static string DefaultIdFieldName = "_id";
        public static List<TableColumn> CreateOrUpdateSchema(TableContext tableContext,IDictionary<string,object> newSchema)
        {
            var list = new List<TableColumn>();
            var schema = GetSchemaFromDb(tableContext.TableName,tableContext.ConnectionString);

            if (schema == null || schema.Count==0)
            {
                foreach(var column in newSchema)
                {
                    list.Add(new TableColumn(column));
                }
                
                //add systemPrimary primary key
                if (!list.Exists(l => l.Name == DefaultIdFieldName))
                {
                    list.Insert(0, new TableColumn
                    {
                        Name=DefaultIdFieldName,
                        DataType=TypeConverter.GetDataType(typeof(Guid)),
                        IsPrimary=true
                    });
                }

                CreateTableSchema(tableContext,list);
            }
            else
            {
                list = CompareSchema(schema, newSchema);
                UpdateTableSchema(tableContext, list);
            }

            return list;

        }

        public static List<TableColumn> GetSchemaFromDb(string tableName, string connectionString)
        {
            try
            {
                var connection = DataBus.GetDataBase(connectionString);
                var list = connection.GetScheme<object>(tableName);

                return list.Select(l => new TableColumn(l as IDictionary<string, object>)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static void CreateTableSchema(TableContext tableContext, List<TableColumn> columns)
        {
            var sql = GetCreateTableSql(tableContext.TableName, columns);

            var connection = DataBus.GetDataBase(tableContext.ConnectionString);
            connection.ExecuteNoQuery(sql,null);
        }

        private static void UpdateTableSchema(TableContext tableContext,List<TableColumn> list)
        {
            var sb = new StringBuilder();
            var columns = list.FindAll(c => c.IsChange);
            foreach (var column in columns)
            {
                sb.Append(GetAddOrUpdateColumnSql(tableContext.TableName, column, column.IsNew));
                sb.Append(";");
            }
            var sql = sb.ToString();
            if(!string.IsNullOrEmpty(sql))
            {
                var connection = DataBus.GetDataBase(tableContext.ConnectionString);
                connection.ExecuteNoQuery(sql, null);
            }
        }

        private static string GetAddOrUpdateColumnSql(string tableName,TableColumn column,bool isNew)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("alter table {0} {1} column {2} {3}", tableName, isNew ? "add" : "modify", column.Name, column.DataType);

            return builder.ToString();
        }

        private static string GetCreateTableSql(string tableName, List<TableColumn> columns)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("create table " + tableName);
            builder.Append("(");

            var index = 0;
            foreach (var column in columns)
            {
                builder.AppendFormat("{0} {1}", column.Name, column.DataType);
                if (column.IsPrimary)
                {
                    builder.Append(" PRIMARY KEY");
                }
                if (index != columns.Count - 1)
                {
                    builder.Append(",");
                }
                index++;
            }
            builder.Append(")");

            return builder.ToString();
        }

        

        private static List<TableColumn> CompareSchema(List<TableColumn> schemaList, IDictionary<string,object> newSchema)
        {

            var newSchemaList = new List<TableColumn>();
            foreach (var item in newSchema)
            {
                newSchemaList.Add(new TableColumn(item));
            }

            var result = new List<TableColumn>();

            foreach(var newcolumn in newSchemaList)
            {
                var column = schemaList.Find(s => s.Name.Equals(newcolumn.Name, StringComparison.OrdinalIgnoreCase));
                if (column==null)
                {
                    newcolumn.IsNew = true;
                }
                //I think we can't change the user's table type.
                //if the column type is not the same with the system generated type,it means that user changes the datatype 
                // and our system generated type is not meetting the needs of user.
                //temp our system data type is bool,varchar(255),double,char(36),datetime,bigint(20)

                //else if (newcolumn.DataType != column.DataType)
                //{
                //    column.IsChange = true;
                //    column.DataType = newcolumn.DataType;
                //    result.Add(column);
                //}

                result.Add(newcolumn);

            }

            if (result.Count == 0)
            {
                result.AddRange(schemaList);
            }
            else
            {
                foreach(var column in schemaList)
                {
                    if (!result.Exists(c => c.Name.Equals(column.Name,StringComparison.OrdinalIgnoreCase)))
                    {
                        result.Add(column);
                    }
                }
            }
            return result;

        }
    }


   
}

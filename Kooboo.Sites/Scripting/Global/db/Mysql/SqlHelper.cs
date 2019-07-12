using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Sites.Scripting.Global.Db
{
    public class SQLHelper
    {
        public static string GetInsertSql(string tableName, IDictionary<string, object> dic)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("insert into {0}", tableName);

            var fieldBuilder = new StringBuilder();
            var valueBuilder = new StringBuilder();
            foreach (var keypair in dic)
            {
                if (fieldBuilder.Length > 0)
                    fieldBuilder.Append(",");
                fieldBuilder.AppendFormat("{0}", keypair.Key);

                if (valueBuilder.Length > 0)
                    valueBuilder.Append(",");
                valueBuilder.AppendFormat("@{0}", keypair.Key);
            }

            builder.AppendFormat("({0})", fieldBuilder.ToString());
            builder.AppendFormat("values({0})", valueBuilder.ToString());

            builder.Append(";select @@IDENTITY");
            return builder.ToString();
        }

        public static string GetUpdateSql(string tableName, IDictionary<string, object> dic, string primaryKey)
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

        public static string GetDeleteSql(string tableName, string primaryKey)
        {
            return string.Format("delete from {0} where {1}=@{1}", tableName, primaryKey);
        }

        public static string GetSelectSql(string tableName, string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return string.Format("select * from {0}", tableName);
            }
            return string.Format("select * from {0} where {1}", tableName, condition);
        }

        public static string CreateIndexSql(string tableName, string field)
        {
            return string.Format("ALTER TABLE {0} ADD INDEX {0}_{1} ({1})", tableName, field);
        }

        public static string GetCountSql(string tableName, string condition)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("select count(1) as count from {0} ",tableName);

            if (!string.IsNullOrEmpty(condition))
            {
                builder.AppendFormat("where {0}", condition);
            }
            return builder.ToString();
        }

        public static string GetSelectSql(string tableName, string condition, string orderBy, bool Ascending, int skipcount, int count)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("select * from {0}", tableName);
            if (!string.IsNullOrEmpty(condition))
            {
                builder.AppendFormat(" where {0}", condition);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                builder.AppendFormat(" order by {0} {1}", orderBy, Ascending ? "asc" : "desc");
            }

            builder.AppendFormat(" limit {0},{1}", skipcount, count);

            return builder.ToString();
        }



    }
}

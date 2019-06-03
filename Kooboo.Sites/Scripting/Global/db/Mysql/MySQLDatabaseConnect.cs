using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Configuration;
using System.Data.Common;

namespace Kooboo.Sites.Scripting.Global.Db
{

    public interface IDatabaseConnect
    {
        void BeginTransaction();

        void Commit();

        void RollBack();

        bool ExecuteNoQuery(string sql, IDictionary<string,object> parameters);

        void ExecuteNoQuery<T>(string sql, T model);
        //get auto increase data
        object ExecuteScalar(string sql, IDictionary<string, object> parameters);

        List<T> ExecuteList<T>(string sql, IDictionary<string, object> parameters);

        T ExecuteSingle<T>(string sql, IDictionary<string, object> parameters);
    }
    public class MySQLDatabaseConnect: IDatabaseConnect
    {
        private MySqlConnection sqlConnection = null;

        private string ConnectString = "";

        public MySQLDatabaseConnect(string connectString)
        {
            ConnectString = connectString;
        }

        private MySqlConnection Connection
        {
            get
            {
                if (sqlConnection == null)
                {
                    sqlConnection = new MySqlConnection(ConnectString);
                }
                return sqlConnection;
            }
            set { sqlConnection = null; }
        }

        #region Reserved method

        private bool Open()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new MySqlConnection(ConnectString);
                sqlConnection.Open();
            }
            else if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            else if (sqlConnection.State == ConnectionState.Broken)
            {
                sqlConnection.Close();
                sqlConnection = new MySqlConnection(ConnectString);
                sqlConnection.Open();
            }
            return sqlConnection.State == ConnectionState.Open;
        }

        private void Close()
        {
            if (!isTransactionRun)
                Dispose();
        }

        private IDbTransaction transaction;
        protected IDbTransaction Transaction
        {
            get
            {
                if (null == transaction)
                    BeginTransaction();
                return transaction;
            }
        }
        
        int transactionCount = 0;
        private bool isTransactionRun;
        private bool isRollBack = false;

        public void BeginTransaction()
        {
            if (null == transaction)
            {
                try
                {
                    Open();
                    transaction = this.Connection.BeginTransaction();
                    isTransactionRun = true;
                }
                catch (Exception ex)
                {
                    Close();
                    throw ex;
                }
            }
            transactionCount++;
        }

        public void Commit()
        {
            transactionCount--;
            if (transactionCount <= 0)
            {
                try
                {
                    if (!isRollBack)
                        this.transaction.Commit();
                    else
                        this.transaction.Rollback();
                }
                finally
                {
                    DisposeTransaction();
                }
            }
        }

        public void Dispose()
        {
            MySqlConnection conn = this.Connection;
            if (conn != null && !isTransactionRun)		
            {
                this.Connection = null;
                transactionCount = 0;
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                conn.Dispose();
            }
        }

        public void RollBack()
        {
            isRollBack = true;
            transactionCount--;
            if (transactionCount <= 0)
            {
                try
                {
                    this.transaction.Rollback();
                }
                finally
                {
                    DisposeTransaction();
                }
            }
        }

        private void DisposeTransaction()
        {
            try
            {
                if (null != transaction)
                    transaction.Dispose();
                transaction = null;
                isTransactionRun = false;
                if (transactionCount != 0)
                    transactionCount = 0;
                if (isRollBack)
                    isRollBack = false;
                Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// <summary>
        /// create,update,Delete operate
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public  bool ExecuteNoQuery(string sql, IDictionary<string, object> value)
        {
            var param = GetParam(value);
            int result= Connection.Execute(sql, param, transaction);
            return result > 0;
        }

        /// <summary>
        ///  create,update,Delete operate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="model"></param>
        public void ExecuteNoQuery<T>(string sql, T model)
        {
            Connection.Execute(sql, model, transaction);
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, IDictionary<string, object> value)
        {
            return Connection.ExecuteScalar(sql, value);
        }
        /// <summary>
        /// return a list 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> ExecuteList<T>(string sql, IDictionary<string, object> value)
        {
            var param = GetParam(value);
            return Connection.Query<T>(sql, param, transaction).AsList();
        }
        /// <summary>
        /// return single Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteSingle<T>(string sql, IDictionary<string, object> value)
        {
            //return (T) Connection.QuerySingleOrDefault(sql, parameters);
            var param = GetParam(value);
            return (T)Connection.QueryFirstOrDefault<T>(sql, param, transaction);
        }

        private DynamicParameters GetParam(IDictionary<string,object> value)
        {
            var param = new DynamicParameters();
            if (value != null)
            {
                foreach(var keypair in value)
                {
                    param.Add(keypair.Key, keypair.Value);
                }
            }
            return param;
        }

    }


    /// <summary>
    /// Different thread get different DataBaseConnect
    /// </summary>
    public class DataBus
    {
        [ThreadStatic]
        private static IDatabaseConnect ThreadDatabaseConnect;

        public static IDatabaseConnect GetDataBase(string connectionString)
        {
            if (ThreadDatabaseConnect == null)
            {
                ThreadDatabaseConnect = new MySQLDatabaseConnect(connectionString);

            }
            return ThreadDatabaseConnect;
        }
    }
}

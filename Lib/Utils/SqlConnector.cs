// Copyright Joy Developing.

using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tools.Utils.SQL
{
    /// <summary>
    /// Class for support interoperating with <c>Microsoft SQL Server 2005</c>.
    ///
    /// <b>WARNING!</b>
    /// Other databases are not supported and never will be.
    /// </summary>
    public class SqlConnector
    {
        /// <summary>
        /// Connection.
        /// </summary>
        private SqlConnection Connection;

        /// <summary>
        /// Connection string.
        /// </summary>
        public string ConnectionString;

        /// <summary>
        /// Transaction.
        /// </summary>
        private SqlTransaction Transaction;

        /// <summary>
        /// Data set.
        /// </summary>
        private DataSet Set;

        /// <summary>
        /// Adapters collection.
        /// </summary>
        private Dictionary<string, SqlDataAdapter> AdaptersDict;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SqlConnector()
        {
            AdaptersDict = new Dictionary<string, SqlDataAdapter>();
            Set = new DataSet();
        }

        /// <summary>
        /// Connect to database.
        /// </summary>
        public void Connect()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        /// <summary>
        /// Connect to database.
        /// </summary>
        /// <param name="connection_string">string</param>
        public void Connect(string connection_string)
        {
            ConnectionString = connection_string;
            Connect();
        }

        /// <summary>
        /// Disconnect.
        /// </summary>
        public void Disconnect()
        {
            Transaction.Rollback();
            Connection.Close();
        }

        /// <summary>
        /// Transaction commit.
        /// </summary>
        public void Commit()
        {
            Transaction.Commit();
            Transaction = Connection.BeginTransaction();
        }

        /// <summary>
        /// Transaction rollback.
        /// </summary>
        public void Rollback()
        {
            Transaction.Rollback();
            Transaction = Connection.BeginTransaction();
        }

        /// <summary>
        /// Scalar query.
        /// </summary>
        /// <param name="query">query</param>
        /// <returns>result</returns>
        public Object ExecuteScalar(string query)
        {
            SqlCommand command = new SqlCommand(query, Connection, Transaction);
            command.CommandType = CommandType.Text;

            return command.ExecuteScalar();
        }

        /// <summary>
        /// Check if has adapter.
        /// </summary>
        /// <param name="name">adapter name</param>
        /// <returns>result</returns>
        private bool HasAdapter(string name)
        {
            return AdaptersDict.ContainsKey(name);
        }

        /// <summary>
        /// Add adapter.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>added adapter</returns>
        private SqlDataAdapter AddAdapter(string name)
        {
            if (!HasAdapter(name))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.TableMappings.Add("Table", name);
                AdaptersDict.Add(name, adapter);

                return adapter;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Delete adapter.
        /// </summary>
        /// <param name="name">name</param>
        public void DeleteAdapter(string name)
        {
            if (HasAdapter(name))
            {
                AdaptersDict.Remove(name);
            }
        }

        /// <summary>
        /// Get adapter.
        /// <param name="name">name</param>
        /// <returns>adapter</returns>
        /// </summary>
        private SqlDataAdapter GetAdapter(string name)
        {
            SqlDataAdapter adapter;

            if (HasAdapter(name))
            {
                AdaptersDict.TryGetValue(name, out adapter);
            }
            else
            {
                adapter = null;
            }

            return adapter;
        }

        /// <summary>
        /// Set command <c>select</c> for adapter.
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="query">query</param>
        public void SetSelectCommand(string name, string query)
        {
            SqlDataAdapter adapter;
            SqlCommand command;

            AddAdapter(name);
            adapter = GetAdapter(name);
            command = new SqlCommand(query, Connection, Transaction);
            command.CommandType = CommandType.Text;
            adapter.SelectCommand = command;
        }

        /// <summary>
        /// Fill data set.
        /// </summary>
        /// <param name="name">adapter name</param>
        public void Fill(string name)
        {
            SqlDataAdapter adapter = GetAdapter(name);

            if (adapter != null)
            {
                adapter.Fill(Set);
            }
        }

        /// <summary>
        /// Link adapter to table.
        /// </summary>
        /// <param name="dgv">data grid view</param>
        /// <param name="name">adapter name</param>
        public void Link(DataGridView dgv, string name)
        {
            dgv.DataSource = Set;
            dgv.DataMember = name;
        }

        /// <summary>
        /// Update changes to database.
        /// </summary>
        /// <param name="name">adapter name</param>
        public void Update(string name)
        {
            SqlDataAdapter adapter = GetAdapter(name);

            if (adapter != null)
            {
                adapter.Update(Set);
            }
        }
    }
}

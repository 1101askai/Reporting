using System;
using System.Data;
using System.Data.SqlClient;

namespace BankingReport.Helpers
{
    public class Dapper : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dapper"/> class.
        /// </summary>
        public Dapper()
        {
            var open = Connection;
        }

        /// <summary>
        /// Gets the open connection.
        /// </summary>
        /// <returns></returns>
        private static SqlConnection GetOpenConnection()
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Gets the closed connection.
        /// </summary>
        /// <returns>SqlConnection.</returns>
        /// <exception cref="System.InvalidOperationException">should be closed!</exception>
        private static SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Connection?.Dispose();
        }

        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection _connection;

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public SqlConnection Connection => _connection ?? (_connection = GetOpenConnection());
    }
}
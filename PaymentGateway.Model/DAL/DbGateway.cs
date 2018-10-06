
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PaymentGateway.Model.DAL
{
    /// <summary>
    /// Helper class to make DB insertions.
    /// </summary>
    public class ExecValuePair
    {
        public ExecValuePair(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }

    /// <summary>
    /// Database access class.
    /// </summary>
    public class DbGateway
    {
        private const string RELATIVE_PATH = @"PaymentGateway.Model\DAL\";

        /// <summary>
        /// Class constructor, initializes the connection string
        /// </summary>
        public DbGateway(string dbName = "DbGateway.mdf")
        {
            var solution_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\");
            var db_path = Path.GetFullPath(Path.Combine(solution_path, RELATIVE_PATH + dbName));

            connectionString =
                $@"Data Source=(LocalDB)\MSSQLLocalDB;
                  AttachDbFilename={db_path};
                  Integrated Security = True; 
                  Connect Timeout = 30";
        }


        /// <summary>
        /// Connection string to establish the connection with the database.
        /// </summary>
        private string connectionString { get; private set; }


        /// <summary>
        /// Reads a query string and returns the resulting DataTable
        /// </summary>
        /// <param name="sqlQuery">Sql query</param>
        /// <returns>A DataTable with the query results</returns>
        public DataTable Read(string sqlQuery)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var adapter = new SqlDataAdapter(sqlQuery, connection))
            {
                var result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }

        /// <summary>
        /// Runs an query (insert, update or delete).
        /// </summary>
        public void Exec(string sqlQuery, IEnumerable<ExecValuePair> pairs)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sqlQuery, connection))
            {
                connection.Open();
                foreach (var pair in pairs)
                    cmd.Parameters.AddWithValue(pair.Name, pair.Value);

                // Execute
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }


    } //class DbGateway
} //namespace

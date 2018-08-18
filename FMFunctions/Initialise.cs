using System;
using System.Data;
using System.Data.SqlClient;

namespace FMFunctions
{
    public class Initialise : IDisposable
    {
        public IDbConnection Connection { get; set; }

        public Initialise(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}

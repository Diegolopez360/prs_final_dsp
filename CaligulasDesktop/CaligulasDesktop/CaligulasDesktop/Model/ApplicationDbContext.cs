using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace CaligulasDesktop.Model
{
    class ApplicationDbContext
    {
        private SqlConnection _connection;

        public ApplicationDbContext()
        {
            try
            {
                _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CaligulasConnection"].ConnectionString);
                _connection.Open();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
        }

        protected SqlConnection Open()
        {
            return _connection.State == System.Data.ConnectionState.Open ? _connection : null;
        }

        protected void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}

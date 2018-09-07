using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VendaDireta.Models
{
    public class MySqlConnection : IDisposable
    {
        protected SqlConnection conn;

        public MySqlConnection()
        {
            string strConn = @"Data Source=127.0.0.1;
                               Initial Catalog=BDVendaDireta;
                               Integrated Security=true";
            //                   Integrated Security=false; User Id = sa; Password = dba";

            conn = new SqlConnection(strConn);
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }
    }
}

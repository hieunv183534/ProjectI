using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Infrastructure.Repository
{
    public class DatabaseConnection
    {
        public static IDbConnection DbConnection
        {
            get { return new SqlConnection("Server=DESKTOP-LNGJ4BJ\\SQLEXPRESS; User=sa; Password=123; Database=CompanyManagement"); }
        }
    }
}

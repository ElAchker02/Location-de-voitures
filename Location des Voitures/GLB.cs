using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Location_des_Voitures
{
    public static class GLB
    {
        public static string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=projet_location_voitures;Integrated Security=True";
        public static SqlConnection Connection = new SqlConnection(ConnectionString);
        public static SqlCommand Cmd;
        public static SqlDataReader dr;
        public static DataSet ds = new DataSet();
        public static DataTable dtImperssion = new DataTable();
        public static DataTable dtRetour = new DataTable();
        public static SqlDataAdapter da;
        public static float Total;
    }
}

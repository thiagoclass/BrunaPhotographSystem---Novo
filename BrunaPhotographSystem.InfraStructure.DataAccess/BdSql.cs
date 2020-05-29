using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BrunaPhotographSystem.InfraStructure.DataAccess
{
        public static class BDSQL
        {
            private static SqlConnection con;


            public static SqlConnection AbrirConexao()
            {
            if (con == null)
            {
                
                con = new SqlConnection(Properties.Resources.ConnectionString);
            }
                if (con.State == System.Data.ConnectionState.Closed)
                { con.Open(); }

                return con;
            }

            public static void FecharConexao()
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                { con.Close(); }
            }

            public static void ExecutarComandoSQL(string strComando)
            {
                SqlConnection con = BDSQL.AbrirConexao();

                using (SqlCommand command = new SqlCommand(strComando, con))
                { command.ExecuteNonQuery(); }
            
            BDSQL.FecharConexao();
            }

        public static void ExecutarComandoSQL(string strComando, SqlParameter[] parametros)
        {
            FecharConexao();
            SqlConnection con = BDSQL.AbrirConexao();

            using (SqlCommand command = new SqlCommand(strComando, con))
            {
                command.Parameters.AddRange(parametros);
                command.ExecuteNonQuery(); }

            BDSQL.FecharConexao();
        }


        public static List<Dictionary<string, string>> ExecutarComandoLeituraSQL(string strComando)
            {

                List<Dictionary<string, string>> registros = new List<Dictionary<string, string>>();
                SqlConnection con = BDSQL.AbrirConexao();

                using (SqlCommand command = new SqlCommand(strComando, con))
                 
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> registro = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            registro.Add(reader.GetName(i), reader[reader.GetName(i)].ToString());
                        }
                        registros.Add(registro);
                    }
                }

                BDSQL.FecharConexao();

                return registros;
            }
        public static DataTable ExecutarComandoLeituraSQLDataTable(string strComando)
        {
            var dt = new DataTable();


            SqlConnection con = BDSQL.AbrirConexao();

            using (SqlCommand command = new SqlCommand(strComando, con))

            using (SqlDataReader reader = command.ExecuteReader())
            {
                dt.Load(reader);
            }
        
            BDSQL.FecharConexao();

            return dt;
        }
        public static SqlDataReader ExecutarComandoLeituraSQLtoReader(string strComando)
        {

            SqlConnection con = BDSQL.AbrirConexao();
            SqlDataReader reader;
            SqlCommand command = new SqlCommand(strComando, con);
            reader = command.ExecuteReader();
            
            return reader;
        }
    }
        
}

    



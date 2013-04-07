using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DB
{
    public class Connexion
    {
        static string path = "Server=YXXX\\SQLEXPRESS;Database=PICASA;Integrated Security=true;";
        static SqlConnection connection;

        public static SqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        static public void open()
        {
            connection = new SqlConnection(path);
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :" + e.Message);
            }
        }

        static public void close()
        {
            connection.Close();
        }

        static public bool execute_Request(String str)
        {
            open();
            int resultat = new SqlCommand(str, connection).ExecuteNonQuery();
            close();
            return Convert.ToBoolean(resultat);
        }

        static public SqlDataReader execute_Select(String str)
        {
            open();
            SqlDataReader resultat = new SqlCommand(str, connection).ExecuteReader(CommandBehavior.SequentialAccess);
            return resultat;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace työaika
{
    public class Kanta
    {
        private string connStr;//yhteysmerkkijono
        private SqlConnection conn;//yhteys
        private SqlCommand cmd;//komento


        public Kanta(string p_connStr)
        {
            connStr = p_connStr;

        }
        public bool luoYhteys()
        {
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();//avataan yhteys;
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void suljeYhteys()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
        public bool suoritaPaivitys(string sql_lause)
        {
            try
            {
                if (conn != null)
                {
                    cmd = new SqlCommand(sql_lause, conn);
                    cmd.ExecuteReader();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public SqlDataReader suoritaKysely(string sql_lause)
        {
            SqlDataReader reader = null;//'kyselyn tulosjoukko'
            try
            {
                if (conn != null)
                {
                    cmd = new SqlCommand(sql_lause, conn);
                    reader = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return reader;
        }

    }
}



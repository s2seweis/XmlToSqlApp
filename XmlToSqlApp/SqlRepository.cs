using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace XmlToSqlApp
{
    public class SqlRepository
    {
        private string connectionString = "Data Source=NB242F34R44\\SQLEXPRESS;Initial Catalog=XmlToSqlApp1;User ID=sa;Password=alk123;TrustServerCertificate=True";

        // Constructor
        public SqlRepository()
        {
            CheckAndCreateTable();

        }

        // Method to check for table existence and create if not exists
        private void CheckAndCreateTable()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'XmlDataMLC')
                    BEGIN
                       CREATE TABLE XmlData (
                       [hlp_id] nvarchar(150) NOT NULL PRIMARY KEY,
                       [hlp_title] nvarchar(150) NULL,
                       [hlp_text] nvarchar(MAX) NULL,
                       [hlp_verweisid] nvarchar(150) NULL,
                       [hlp_forceupd] nvarchar(150) NULL,
                       );
                    END";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Method to save data to the database
        public void SaveData(List<Item> items)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (var item in items)
                {
                    string query = "INSERT INTO XmlDataMLC (hlp_id, hlp_title, hlp_text, hlp_verweisid, hlp_forceupd) VALUES (@hlp_id,@hlp_title,@hlp_text,@hlp_verweisid,@hlp_forceupd)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@hlp_id", item.Hlp_id ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@hlp_title", item.Hlp_title ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@hlp_text", item.Hlp_text ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@hlp_verweisid", item.Hlp_verweisid ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@hlp_forceupd", item.Hlp_forceupd ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    // Example Item class; you may already have this defined

}

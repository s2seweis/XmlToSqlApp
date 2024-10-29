using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace XmlToSqlApp
{
    public class SqlRepository
    {
        // Connection string to connect to the SQL database
        private string connectionString = "Data Source=NB242F34R44\\SQLEXPRESS;Initial Catalog=XmlToSqlApp1;User ID=sa;Password=alk123;TrustServerCertificate=True";

        // Constructor
        public SqlRepository()
        {
            // Calls the method to check for the table's existence and create it if it doesn't exist
            CheckAndCreateTable();
        }

        // Method to check for the existence of the table and create it if it does not exist
        private void CheckAndCreateTable()
        {
            // Establishes a connection to the SQL database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open(); // Opens the connection

                // SQL query to check for the existence of the table and create it if not found
                string query = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'XmlDataMLC')
                    BEGIN
                       CREATE TABLE XmlData (
                       [hlp_id] nvarchar(150) NOT NULL PRIMARY KEY,
                       [hlp_title] nvarchar(150) NULL,
                       [hlp_text] nvarchar(MAX) NULL,
                       [hlp_verweisid] nvarchar(150) NULL,
                       [hlp_forceupd] nvarchar(150) NULL
                       ); -- Note: A comma after the last column should be removed
                    END";

                // Executes the SQL command to create the table
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to save data from a list of items to the database
        public void SaveData(List<Item> items)
        {
            // Establishes a connection to the SQL database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open(); // Opens the connection

                // Iterates over each item in the list
                foreach (var item in items)
                {
                    // SQL query to insert a new record into the XmlDataMLC table
                    string query = "INSERT INTO XmlDataMLC (hlp_id, hlp_title, hlp_text, hlp_verweisid, hlp_forceupd) VALUES (@hlp_id, @hlp_title, @hlp_text, @hlp_verweisid, @hlp_forceupd)";

                    // Prepares the SQL command for execution
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Adds parameters to the SQL command, replacing null values with DBNull
                        cmd.Parameters.AddWithValue("@hlp_id", item.Hlp_id ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@hlp_title", item.Hlp_title ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@hlp_text", item.Hlp_text ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@hlp_verweisid", item.Hlp_verweisid ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@hlp_forceupd", item.Hlp_forceupd ?? (object)DBNull.Value);

                        // Executes the insert command
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    // Example Item class; you may already have this defined
}

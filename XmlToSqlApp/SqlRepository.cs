using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace XmlToSqlApp
{
    public class SqlRepository
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\SWE\\source\\repos\\XmlToSqlApp\\XmlToSqlApp\\XmlToSqlApp.mdf;Integrated Security=True; Connect Timeout=30";

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
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'XmlData')
                    BEGIN
                        CREATE TABLE XmlData (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            Name NVARCHAR(100),
                            Value NVARCHAR(100)
                        );
                    END";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to save data to the database
        public void SaveData(List<Item> items)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (var item in items)
                {
                    string query = "INSERT INTO XmlData (Name, Value) VALUES (@Name, @Value)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@Value", item.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    // Example Item class; you may already have this defined
 
}

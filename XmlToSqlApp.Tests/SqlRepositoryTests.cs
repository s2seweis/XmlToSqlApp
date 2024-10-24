using System;
using System.Data.SqlClient;
using Xunit;

namespace XmlToSqlApp.Tests
{
    public class SqlRepositoryTests : IDisposable
    {
        private readonly string _connectionString;

        public SqlRepositoryTests()
        {
            _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\SWE\\source\\repos\\XmlToSqlApp\\XmlToSqlApp\\XmlToSqlApp.mdf;Integrated Security=True; Connect Timeout=30";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var command = new SqlCommand(@"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'XmlData')
                    BEGIN
                        CREATE TABLE XmlData (
                            Id INT IDENTITY(1,1) PRIMARY KEY, 
                            Name NVARCHAR(100), 
                            Value NVARCHAR(100)
                        );
                    END", con);
                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var command = new SqlCommand(@"
                    IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'XmlData')
                    BEGIN
                        DROP TABLE XmlData;
                    END", con);
                command.ExecuteNonQuery();
            }
        }

        [Fact]
        public void TestDatabaseConnection()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                Assert.True(con.State == System.Data.ConnectionState.Open);
            }
        }

        [Fact]
        public void TestTableCreation()
        {
            // Ensure the table is created
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var command = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'XmlData'", con);
                var count = (int)command.ExecuteScalar();
                Assert.Equal(1, count); // Ensure the table exists
            }
        }

        [Fact]
        public void TestTableDeletion()
        {
            // Cleanup to ensure the table doesn't exist before the test
            Dispose();

            // Ensure the table does not exist after disposing
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var command = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'XmlData'", con);
                var count = (int)command.ExecuteScalar();
                Assert.Equal(0, count); // Ensure the table does not exist
            }
        }
    }
}

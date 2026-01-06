using System;
using MySqlConnector;

namespace BankingApplication.Data
{
    public static class DbConnectionFactory
    {
        private static readonly string _connectionString = "Server=localhost;" +
                                                         "Database=banking_app;" +
                                                         "User=root;" +
                                                         "Password=Kihtrak@0329;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}

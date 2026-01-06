using System;
using MySqlConnector;
using BankingApplication.Models;

namespace BankingApplication.Data
{
    public class UserRepository
    {
        public User? GetUserDetails(string email)
        {
            using var connection=DbConnectionFactory.GetConnection();
            connection.Open();

            const string query = @"select * from Users where email=@email;";
            using var command= new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@email", email);

            using var reader=command.ExecuteReader();

            if (!reader.Read())
                return null;

            return new User
            {
                userId = reader.GetInt32("user_Id"),
                email = reader.GetString("email"),
                password = reader.GetString("password")
            };
        }

        public int? CreateUserAccount(string email, string password, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query1 = "insert into users(email,password) values(@email,@password); select Last_Insert_Id();";
            using var command1 = new MySqlCommand(query1, connection, transaction);
            command1.Parameters.AddWithValue("@email",email);
            command1.Parameters.AddWithValue("@password", password);

            return Convert.ToInt32(command1.ExecuteScalar());
        }

    }   
}

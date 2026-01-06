using System;
using MySqlConnector;
using BankingApplication.Models;

namespace BankingApplication.Data
{
    public class UserRepository
    {
        public User? GetByEmail(string email)
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

        public int? CreateUserAccount(string email, string password)
        {
            using var connection=DbConnectionFactory.GetConnection();
            connection.Open();

            const string query1 = "insert into users(email,password) values(@email,@password);";
            using var command1 = new MySqlCommand(query1, connection);
            command1.Parameters.AddWithValue("@email",email);
            command1.Parameters.AddWithValue("@password", password);

            command1.ExecuteReader();

            const string query2 = "select user_Id from users where email=@email and password=@password;";
            using var command2 = new MySqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@email", email);
            command2.Parameters.AddWithValue("@password", password);

            using var reader=command2.ExecuteReader();
            if (!reader.Read()) return null;
            return reader.GetInt32("user_Id");


        }
    }   
}

using System;
using MySqlConnector;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.Data
{
    public class AccountRepository 
    {
        public void CreateAccount(Account account, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query = @"insert into accounts(user_id, account_type, balance, created_at) values(@user_id, @account_type, @balance, @created_at);";
            using var command=new MySqlCommand(query, connection,transaction);
            command.Parameters.AddWithValue("@user_id",account.userId);
            command.Parameters.AddWithValue("@account_type", account.accountType);
            command.Parameters.AddWithValue("@balance", account.balance);
            command.Parameters.AddWithValue("@created_at",account.createdAt);

            command.ExecuteNonQuery();
        }
        public Account GetAccountByAccountId(int accId, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query = @"select * from accounts where account_id=@account_id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@account_id", accId);

            using var reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return new Account
            {
                accountId = reader.GetInt32("account_id"),
                userId = reader.GetInt32("user_id"),
                accountType = reader.GetString("account_type"),
                balance = reader.GetDecimal("balance"),
                createdAt = reader.GetDateTime("created_at")
            };
        }

        public List<Account> GetAccountsByUserId(int userId)
        {
            List<Account> accounts = new List<Account>();

            using var connection=DbConnectionFactory.GetConnection();
            connection.Open();

            const string query = @"select * from accounts where user_id=@user_id;";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", userId);

            using var reader=command.ExecuteReader();
            while (reader.Read())
            {
                Account currAccount = new Account();
                currAccount.accountId = reader.GetInt32("account_id");
                currAccount.userId = reader.GetInt32("user_id");
                currAccount.accountType = reader.GetString("account_type");
                currAccount.balance = reader.GetDecimal("balance");
                currAccount.createdAt = reader.GetDateTime("created_at");

                accounts.Add(currAccount);
            }
            return accounts;
        }


        public void UpdateAccount(Account account, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query = @"update accounts set account_type=@account_type, balance=@balance where account_id=@account_id and user_id=@user_id;";
            using var command = new MySqlCommand(query,connection,transaction);
            command.Parameters.AddWithValue("@account_id",account.accountId);
            command.Parameters.AddWithValue("@user_id",account.userId);
            command.Parameters.AddWithValue("@account_type", account.accountType);
            command.Parameters.AddWithValue("@balance",account.balance);

            command.ExecuteNonQuery();
        }

        public void DeleteAccount(Account account, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query = @"delete from accounts where account_id=@account_id and user_id=@user_id;";
            using var command = new MySqlCommand(query,connection,transaction);
            command.Parameters.AddWithValue("@account_id", account.accountId);
            command.Parameters.AddWithValue("@user_id", account.userId);

            command.ExecuteNonQuery();
        }
    }
}

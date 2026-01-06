using System;
using MySqlConnector;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.Data
{
    public class TransactionRepository
    {
        public int? CreateTransaction(Transaction transaction, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query = @"insert into transactions(account_id, transaction_type, amount, reference_transaction_id) values(@account_id,@transaction_type, @amount, @reference_transaction_id);";
            using var command=new MySqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("@account_id",transaction.accountId);
            command.Parameters.AddWithValue("@transaction_type",transaction.transactionType);
            command.Parameters.AddWithValue("@amount", transaction.amount);
            command.Parameters.AddWithValue("@reference_transaction_id", transaction.referenceTransactionId);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public void UpdateTransaction(Transaction transaction, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query = @"update transactions set reference_transaction_id=@reference_transaction_id where transaction_id=@transaction_id;";
            using var command = new MySqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("@reference_transaction_id",transaction.referenceTransactionId);
            command.Parameters.AddWithValue("@transaction_id", transaction.transactionId);

            command.ExecuteNonQuery();
        }
    }
}

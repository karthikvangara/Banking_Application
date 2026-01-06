using System;
using MySqlConnector;
using BankingApplication.Data;
using BankingApplication.Models;
using BankingApplication.Security;
using System.Collections.Generic;

namespace BankingApplication.Services
{
    public class MoneyTransferService
    {
        private AccountRepository _accountRepository;
        private TransactionRepository _transactionRepository;

        public MoneyTransferService(AccountRepository accountRepository, TransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public void TransferMoney(int userId,int sendersAccountId, int recieversAccountId, decimal amount)
        {
            if (sendersAccountId == recieversAccountId) throw new ArgumentException("Invalid Account transfer"); 
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            using var connection = DbConnectionFactory.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try 
            {
                Account senderAccount = _accountRepository.GetAccountByAccountId(sendersAccountId, connection, transaction);
                if (senderAccount == null) throw new ArgumentException("No account found for sender");
                if (senderAccount.userId != userId) throw new Exception("Unauthorized access");

                Account recieverAccount = _accountRepository.GetAccountByAccountId(recieversAccountId, connection, transaction);
                if (recieverAccount == null) throw new ArgumentException("No reciever account found");

                if (senderAccount.balance < amount) throw new ArgumentException("Insufficient Balance");

                senderAccount.balance -= amount;
                _accountRepository.UpdateAccount(senderAccount, connection, transaction);

                recieverAccount.balance += amount;
                _accountRepository.UpdateAccount(recieverAccount, connection, transaction);

                Transaction sendersTranaction = new Transaction();
                sendersTranaction.accountId = sendersAccountId;
                sendersTranaction.transactionType = "Debit";
                sendersTranaction.amount = amount;

                var sendersTransactionId = _transactionRepository.CreateTransaction(sendersTranaction, connection, transaction);
                if (sendersTransactionId == null) throw new Exception("Transaction Failed");

                Transaction recieversTranaction = new Transaction();
                recieversTranaction.accountId = recieversAccountId;
                recieversTranaction.transactionType = "Credit";
                recieversTranaction.amount = amount;

                var recieversTransactionId = _transactionRepository.CreateTransaction(recieversTranaction, connection, transaction);
                if (recieversTransactionId == null) throw new Exception("Transaction Failed");

                sendersTranaction.referenceTransactionId = recieversTransactionId;
                _transactionRepository.UpdateTransaction(sendersTranaction, connection, transaction);
                
                recieversTranaction.referenceTransactionId = sendersTransactionId;
                _transactionRepository.UpdateTransaction(recieversTranaction, connection, transaction);

                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
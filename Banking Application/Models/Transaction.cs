using System;

namespace BankingApplication.Models
{
    public class Transaction
    {
        public int transactionId { get; set; }
        public int accountId { get; set; }
        public string transactionType { get; set; }
        public decimal amount { get; set; }
        public int referenceTransactionId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
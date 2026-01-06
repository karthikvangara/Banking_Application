using System;

namespace BankingApplication.Models
{
    public class Account
    {
        public int accountId { get; set; }
        public int userId { get; set; }
        public string accountType { get; set; }
        public decimal balance { get; set; }
        public DateTime createdAt { get; set; }
    }
}
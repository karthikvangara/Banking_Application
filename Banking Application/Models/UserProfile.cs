using System;

namespace BankingApplication.Models
{
    public class UserProfile
    {
        public int userId { get; set; }
        public string fullName { get; set; }
        public DateTime dob { get; set; }
        public string gender {  get; set; }
        public string phoneNumber { get; set; }
        public string addressLine1 {  get; set; }
        public string addressLine2 { get; set; }
        public string city {  get; set; }
        public string state { get; set; }
        public string postelCode { get; set; }
        public string country { get; set; }
        public string identityType { get; set; }
        public string identityNumber { get; set; }
        public string kycStatus {  get; set; }
        public DateTime createdAt { get; set; }

    }
}

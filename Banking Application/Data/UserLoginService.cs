using System;
using MySqlConnector;
using BankingApplication.Data;
using BankingApplication.Models;
using BankingApplication.Security;

namespace BankingApplication.Services
{
    public class UserLoginService
    {
        private UserRepository _userRepository;
        public UserLoginService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User LoginUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email is required");
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("password is required");

            User user=_userRepository.GetUserDetails(email);
            if (user == null) throw new ArgumentException("Invalid Email");
            if (!PasswordHasher.VerifyPassword(password,user.password)) throw new ArgumentException("Invalid Password");

            return user;
        }
    }
}
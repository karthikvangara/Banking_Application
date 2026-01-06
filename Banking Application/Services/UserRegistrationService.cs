using System;
using MySqlConnector;
using BankingApplication.Data;
using BankingApplication.Models;
using BankingApplication.Security;

namespace BankingApplication.Services
{
    public class UserRegistrationService
    {
        private UserRepository _userRepository;
        private UserProfileRepository _userProfileRepository;
        public UserRegistrationService(UserRepository userRepository, UserProfileRepository userProfileRepository) 
        {
            _userRepository = userRepository;
            _userProfileRepository=userProfileRepository;
        }
        public void RegisterUser(User user, UserProfile userProfile)
        {

            if(user == null) throw new ArgumentNullException("user detials missing");
            if(userProfile == null) throw new ArgumentNullException("userProfile details missing");
            if (_userRepository.GetUserDetails(user.email) != null) throw new ArgumentException("User Account already exists");

            using var connection=DbConnectionFactory.GetConnection();
            connection.Open();

            using var transaction=connection.BeginTransaction();

            try
            {
                string hashedPassword = PasswordHasher.HashPassword(user.password);
                int userId = _userRepository.CreateUserAccount(user.email, hashedPassword, connection, transaction);
                user.userId = userId;

                userProfile.userId = userId;
                _userProfileRepository.CreateUserProfile(userProfile, connection, transaction);

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
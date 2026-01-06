using System;
using MySqlConnector;
using BankingApplication.Models;
using System.Data;
using System.Transactions;

namespace BankingApplication.Data
{
    public class UserProfileRepository 
    {
        public void CreateUserProfile(UserProfile userProfile, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query = @"INSERT INTO user_profiles
                                (user_id, full_name, dob, gender, phone_number, address_line_1, address_line_2, city, state, postel_code, country, identity_type, identity_number, kyc_status, created_at)
                                VALUES (@user_id, @full_name, @dob, @gender, @phone_number,@address_line_1, @address_line_2, @city, @state, @postel_code, @country, @identity_type, @identity_number, @kyc_status, @created_at);";
            using var command= new MySqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("@user_id",userProfile.userId);
            command.Parameters.AddWithValue("@full_name", userProfile.fullName);
            command.Parameters.AddWithValue("@dob", userProfile.dob);
            command.Parameters.AddWithValue("@gender", userProfile.gender);
            command.Parameters.AddWithValue("@phone_number", userProfile.phoneNumber);
            command.Parameters.AddWithValue("@address_line_1", userProfile.addressLine1);
            command.Parameters.AddWithValue("@address_line_2", userProfile.addressLine2);
            command.Parameters.AddWithValue("@city", userProfile.city);
            command.Parameters.AddWithValue("@state", userProfile.state);
            command.Parameters.AddWithValue("@postel_code", userProfile.postelCode);
            command.Parameters.AddWithValue("@country", userProfile.country);
            command.Parameters.AddWithValue("@identity_type", userProfile.identityType);
            command.Parameters.AddWithValue("@identity_number", userProfile.identityNumber);
            command.Parameters.AddWithValue("@kyc_status", userProfile.kycStatus);
            command.Parameters.AddWithValue("@created_at", userProfile.createdAt);

            command.ExecuteNonQuery();
        }

        public void UpdateUserProfile(UserProfile userProfile, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string query = @"UPDATE user_profiles
                                    SET
                                        full_name = @full_name,
                                        dob = @dob,
                                        gender = @gender,
                                        phone_number = @phone_number,
                                        address_line_1 = @address_line_1,
                                        address_line_2 = @address_line_2,
                                        city = @city,
                                        state = @state,
                                        postel_code = @postel_code,
                                        country = @country,
                                        identity_type = @identity_type,
                                        identity_number = @identity_number,
                                        kyc_status = @kyc_status
                                    WHERE user_id = @user_id;";
            using var command = new MySqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("@user_id", userProfile.userId);
            command.Parameters.AddWithValue("@full_name", userProfile.fullName);
            command.Parameters.AddWithValue("@dob", userProfile.dob);
            command.Parameters.AddWithValue("@gender", userProfile.gender);
            command.Parameters.AddWithValue("@phone_number", userProfile.phoneNumber);
            command.Parameters.AddWithValue("@address_line_1", userProfile.addressLine1);
            command.Parameters.AddWithValue("@address_line_2", userProfile.addressLine2);
            command.Parameters.AddWithValue("@city", userProfile.city);
            command.Parameters.AddWithValue("@state", userProfile.state);
            command.Parameters.AddWithValue("@postel_code", userProfile.postelCode);
            command.Parameters.AddWithValue("@country", userProfile.country);
            command.Parameters.AddWithValue("@identity_type", userProfile.identityType);
            command.Parameters.AddWithValue("@identity_number", userProfile.identityNumber);
            command.Parameters.AddWithValue("@kyc_status", userProfile.kycStatus);
            command.Parameters.AddWithValue("@created_at", userProfile.createdAt);

            command.ExecuteNonQuery();
        }

        public UserProfile? GetUserProfileDetails(int userId)
        {
            using var connection=DbConnectionFactory.GetConnection();
            connection.Open();

            const string query = @"select * from user_profiles where user_id=@userId;";
            using var command=new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);

            using var reader=command.ExecuteReader();
            if (!reader.Read()) return null;
            return new UserProfile
            {
                userId = reader.GetInt32("user_id"),
                fullName = reader.GetString("full_name"),
                dob = reader.GetDateTime("dob"),
                gender = reader.IsDBNull("gender")?null:reader.GetString("gender"),
                phoneNumber = reader.GetString("phone_number"),
                addressLine1 = reader.GetString("address_line_1"),
                addressLine2 = reader.IsDBNull("address_line_2") ? null : reader.GetString("address_line_2"),
                city = reader.GetString("city"),
                state = reader.GetString("state"),
                postelCode = reader.GetString("postel_code"),
                country = reader.GetString("country"),
                identityType = reader.GetString("identity_type"),
                identityNumber = reader.GetString("identity_number"),
                kycStatus = reader.GetString("kyc_status"),
                createdAt = reader.GetDateTime("created_at")
            };
        }
    }
}

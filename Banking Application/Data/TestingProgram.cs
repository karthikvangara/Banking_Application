using System;
using BankingApplication.Data;
using BankingApplication.Models;
public class TestingProgram
{
    public static void Main(string[] args)
    {
        UserRepository userRepository = new UserRepository();
        var userId=userRepository.CreateUserAccount("vangarakarthik29@gmail.com", "Kihtrak@0329");


        UserProfileRepository userProfileRepository = new UserProfileRepository();
        UserProfile userProfile = new UserProfile();

        userProfile.userId = Convert.ToInt32(userId);
        userProfile.fullName = "Karthik";
        userProfile.dob=DateTime.Now;
        userProfile.gender = "Male";
        userProfile.phoneNumber = "8096406391";
        userProfile.addressLine1 = "sdfnk";
        userProfile.addressLine2 = "asdkfn";
        userProfile.city = "Hanamkonda";
        userProfile.state = "Telangana";
        userProfile.postelCode = "506342";
        userProfile.country = "India";
        userProfile.identityType = "Aadhar card";
        userProfile.identityNumber = "832648798";
        userProfile.kycStatus = "success";
        userProfile.createdAt = DateTime.Now;

        userProfileRepository.CreateUserProfile(userProfile);
        Console.WriteLine("User name = " + userProfileRepository.GetUserProfileDetails(userProfile.userId).fullName);

        
    }
}

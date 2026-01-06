using System;
using BankingApplication.Data;
using BankingApplication.Models;
public class TestingProgram
{
    public static void Main(string[] args)
    {
        UserRepository userRepository = new UserRepository();
        var userId = userRepository.CreateUserAccount("vangarakarthik29@gmail.com","Kihtrak@0329");
        if (userId == null) Console.WriteLine("No user created");
        else
        {
            Console.WriteLine("User id : "+userId);
        }
    }
}

using System;
using System.Security.Cryptography;

namespace BankingApplication.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;      // 128-bit
        private const int KeySize = 32;       // 256-bit
        private const int Iterations = 100_000;

        public static string HashPassword(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(
                password,
                SaltSize,
                Iterations,
                HashAlgorithmName.SHA256);

            var salt = Convert.ToBase64String(algorithm.Salt);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));

            return $"{Iterations}.{salt}.{key}";
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.');
            if (parts.Length != 3)
                throw new FormatException("Invalid hashed password format");

            var iterations = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256);

            var keyToCheck = algorithm.GetBytes(KeySize);

            return CryptographicOperations.FixedTimeEquals(
                keyToCheck,
                key);
        }
    }
}

using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Common.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public bool Verify(string password, string hashedPassword, byte[] salt)
        {
            var hash = HashPassword(password, salt);
            return hash == hashedPassword;
        }
    }
}

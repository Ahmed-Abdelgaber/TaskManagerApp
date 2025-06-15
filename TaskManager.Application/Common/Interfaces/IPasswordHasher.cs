namespace TaskManager.Application.Common.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password, byte[] salt);
    byte[] GenerateSalt();
    bool Verify(string password, string hashedPassword, byte[] salt);
}
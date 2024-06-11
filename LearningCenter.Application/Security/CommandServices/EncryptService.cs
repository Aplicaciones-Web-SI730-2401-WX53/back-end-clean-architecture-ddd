using LearningCenter.Domain.Security.Services;

namespace Application.Security.CommandServices;

public class EncryptService : IEncryptService
{
    public string Ecrypt(string password)
    {
       return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
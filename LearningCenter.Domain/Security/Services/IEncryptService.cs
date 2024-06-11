namespace LearningCenter.Domain.Security.Services;

public interface IEncryptService
{
    string Ecrypt(string password);
    bool VerifyPassword(string password, string passwordHash);
}
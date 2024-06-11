using LearningCenter.Domain.Security.Models;

namespace LearningCenter.Domain.Security.Services;

public interface ITokenService
{
    string GenerateToken(User user);
    
    Task<int?> ValidateToken(string token);
}
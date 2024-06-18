using LearningCenter.Domain.Security.Models;

namespace LearningCenter.Domain.Security.Repositories;

public interface IUserRepository 
{
    Task<User> Register(User user);
    Task<User> Login(string email, string password);
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserById(int id);
    Task Update(User user);
    Task Delete(Guid id);
}
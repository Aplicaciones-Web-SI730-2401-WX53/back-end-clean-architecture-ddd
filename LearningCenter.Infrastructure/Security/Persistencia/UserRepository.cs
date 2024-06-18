using LearningCenter.Domain.Security.Models;
using LearningCenter.Domain.Security.Repositories;
using LearningCenter.Infraestructure.Shared.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.Infraestructure.Security.Persistencia;

public class UserRepository : IUserRepository
{
    private readonly LearningCenterContext _learningCenterContext;

    public UserRepository(LearningCenterContext learningCenterContext)
    {
        _learningCenterContext = learningCenterContext;
    }
    
    public async Task<User> Register(User user)
    {
        _learningCenterContext.Users.Add(user);
        await _learningCenterContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> Login(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByUsername(string username)
    {
      var user =  await _learningCenterContext.Users.FirstOrDefaultAsync(x => x.Username == username);
      return user;
    }


    public async Task<User> GetUserById(int id)
    {
        var user =  await _learningCenterContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task Update(User user)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
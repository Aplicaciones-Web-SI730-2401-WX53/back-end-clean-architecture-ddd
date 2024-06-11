using LearningCenter.Domain.Security.Models;
using LearningCenter.Domain.Security.Models.Comands;

namespace LearningCenter.Domain.Security.Services;

public interface IUserCommandService
{
    Task<string> Handle(SigninCommand command);
    Task<User> Handle(SignupCommand command);

}
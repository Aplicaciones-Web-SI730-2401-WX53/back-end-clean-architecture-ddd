using System.Data;
using LearningCenter.Domain.Security.Models;
using LearningCenter.Domain.Security.Models.Comands;
using LearningCenter.Domain.Security.Repositories;
using LearningCenter.Domain.Security.Services;

namespace Application.Security.CommandServices;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptService _encryptService;
    private readonly ITokenService _tokenService;
    
    public UserCommandService(IUserRepository userRepository,IEncryptService encryptService,ITokenService tokenService)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _encryptService = encryptService;
    }

    public async Task<User> Handle(SignupCommand command)
    {
        var exiteingUser = await _userRepository.GetUserByUsername(command.Username);
        if (exiteingUser != null) throw new DuplicateNameException("Username  already exits");

        var user = new User()
        {
            Username = command.Username,
            Role = command.Role,
            PasswordHashed = _encryptService.Ecrypt(command.Password)
        };

         return await _userRepository.Register(user);
    }

    public async Task<string> Handle(SigninCommand  command)
    {
        var existignUser = await _userRepository.GetUserByUsername(command.Username);
        if (existignUser == null) throw new DuplicateNameException("Username  not found");

        if( !_encryptService.VerifyPassword(command.Password, existignUser.PasswordHashed))
            throw new DuplicateNameException("Invalid password or username");
        
        var user = new User(){Id = existignUser.Id, Username = command.Username};
        var token = _tokenService.GenerateToken(user);
        
        return token;
    }
}
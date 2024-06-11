namespace LearningCenter.Domain.Security.Models.Comands;

public record SignupCommand
{
    public string Username { get; init; }
    public string Password { get; init; }
    public string Role { get; init; }
}
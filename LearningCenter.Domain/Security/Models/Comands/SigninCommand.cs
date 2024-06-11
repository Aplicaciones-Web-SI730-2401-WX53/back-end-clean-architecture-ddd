namespace LearningCenter.Domain.Security.Models.Comands;

public record SigninCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
}




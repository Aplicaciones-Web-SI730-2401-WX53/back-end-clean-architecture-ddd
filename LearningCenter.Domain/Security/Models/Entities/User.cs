using Domain;

namespace LearningCenter.Domain.Security.Models;

public class User : ModelBase
{
    public string Username { get; set; }
    public string PasswordHashed { get; set; }
    public string Role { get; set; }
}
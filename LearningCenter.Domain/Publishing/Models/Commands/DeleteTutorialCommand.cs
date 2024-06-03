using System.ComponentModel.DataAnnotations;

namespace Presentation.Request;

public class DeleteTutorialCommand
{
    public int Id { get; set; }
}
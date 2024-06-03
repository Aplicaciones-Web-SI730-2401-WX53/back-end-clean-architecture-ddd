using System.ComponentModel.DataAnnotations;

namespace Presentation.Request;

public class UpdateTutorialCommand
{
    [Required] public int Id { get; set; }
    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] [Range(1990, 2024)] public int Year { get; set; }

    [Required] [Range(1, 100)] public int Quantity { get; set; }

    public List<SectionRequest> Sections { get; set; }
}
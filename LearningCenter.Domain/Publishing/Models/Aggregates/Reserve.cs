using Domain;

namespace LearningCenter.Domain.Publishing.Models.Aggregates;

public class Reserve : ModelBase
{
    public Tutorial Tutorial { get; set; }
    
}
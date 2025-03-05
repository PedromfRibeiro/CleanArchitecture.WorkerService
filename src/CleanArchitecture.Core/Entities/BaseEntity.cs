using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Core.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}

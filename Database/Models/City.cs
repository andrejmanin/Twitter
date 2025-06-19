using System.ComponentModel.DataAnnotations;

namespace Database.Models;

public class City
{
    public Guid Id { get; set; }
    [MaxLength(15)]
    public string Name { get; set; } = string.Empty;
}
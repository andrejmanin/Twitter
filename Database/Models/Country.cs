using System.ComponentModel.DataAnnotations;

namespace Database.Models;

public class Country
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(10)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(4)]
    public string PhoneCode { get; set; } = string.Empty;
}
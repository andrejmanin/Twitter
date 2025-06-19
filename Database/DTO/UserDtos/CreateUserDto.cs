using System.ComponentModel.DataAnnotations;

namespace Database.DTO;

public class CreateUserDto
{
    [Required, EmailAddress]
    public string Gmail { get; set; } = string.Empty;
    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string CityName { get; set; } = string.Empty;
}
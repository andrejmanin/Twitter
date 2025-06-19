namespace Database.DTO;

public class UpdateUserDto
{
    public string Gmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
using Database.Models;

namespace Database.DTO;

public class UserDto
{
    public string Gmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public CountryDto Country { get; set; } = new CountryDto();
    public CityDto City { get; set; } = new CityDto();
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<PostDto> Posts { get; set; } = new List<PostDto>();
    public ICollection<Follower> Followers { get; set; } = new List<Follower>();
    public ICollection<Follower> Following { get; set; } = new List<Follower>();
}
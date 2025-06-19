using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.DTO;

namespace Database.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(255)]
    public string Gmail { get; set; } = string.Empty;
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Nickname { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Surname { get; set; } = string.Empty;
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid CountryId { get; set; }
    [ForeignKey("CountryId")]
    public Country Country { get; set; }
    public Guid CityId { get; set; }
    [ForeignKey("CityId")]
    public City City { get; set; }
    [MaxLength(100)]
    public string Status { get; set; } = string.Empty;
    [MaxLength]
    public string Description { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Follower> Followers { get; set; } = new List<Follower>();
    public ICollection<Follower> Following { get; set; } = new List<Follower>();
}
using System.ComponentModel.DataAnnotations.Schema;
using Client.Models.DTO.UserDtos;

namespace Client.Models.DTO;

public class Follower
{
    public UserDto User { get; set; }
    public UserDto FollowerUser { get; set; }
}
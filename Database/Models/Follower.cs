using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class Follower
{
    public Guid UserId { get; set; }
    [Key, ForeignKey("UserId")]
    public User User { get; set; } = new User();
    public Guid FollowerId { get; set; }
    [ForeignKey("FollowerId")]
    public User FollowerUser { get; set; } = new User();
}
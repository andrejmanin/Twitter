using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class Post
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;
    [MaxLength]
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
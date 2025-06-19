namespace Database.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}
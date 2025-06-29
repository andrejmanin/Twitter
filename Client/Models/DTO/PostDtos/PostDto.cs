namespace Client.Models.DTO.PostDtos;

public class PostDto
{
    public Guid Id { get; set; }
    public string UserNickname { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
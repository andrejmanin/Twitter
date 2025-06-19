namespace Database.DTO.CommentDtos;

public class CommentDto
{
    public Guid PostId { get; set; }
    public string UserNickname { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime PostedOn { get; set; }
}
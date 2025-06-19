namespace Database.DTO.CommentDtos;

public class UpdateCommentDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
}
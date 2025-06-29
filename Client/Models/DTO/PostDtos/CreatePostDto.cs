namespace Client.Models.DTO.PostDtos;

public class CreatePostDto
{
    public string UserNickname { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
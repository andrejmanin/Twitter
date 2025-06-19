namespace Database.DTO;

public class UpdatePostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
}
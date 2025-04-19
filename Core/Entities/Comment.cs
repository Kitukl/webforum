namespace Core.Entities;

public class Comment
{
  public Guid Id { get; set; }
  public User? Creator { get; set; }
  public Discussion Discussion { get; set; }
  public string Content { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
}
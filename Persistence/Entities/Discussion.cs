namespace Persistence.Entities;

public class Discussion
{
  public Guid Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public User? Creator { get; set; }
  public List<Comment> Comments { get; set; }
  public List<string> Categories { get; set; }
}
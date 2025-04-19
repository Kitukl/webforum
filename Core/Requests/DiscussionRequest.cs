namespace Core.Requests;

public class DiscussionRequest
{
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public Guid Creator { get; set; }
  public List<string> Categories { get; set; } = new();
}
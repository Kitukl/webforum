namespace Core.Requests;

public class DiscussionUpdateContentRequest
{
  public Guid id { get; set; }
  public string Content { get; set; }
}
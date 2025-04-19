using Core.Entities;

namespace Core.Response;

public class DiscussionResponse
{
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Content { get; set; }
  public string Username { get; set; }
  public List<Comment> Comments { get; set; }
  public List<string> Categories { get; set; }
  
}
namespace Core.Requests;

public class CommentRequest
{
  public Guid Creator { get; set; }
  public string Content { get; set; }
}
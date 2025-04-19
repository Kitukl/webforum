namespace Core.Requests;

public class DiscussionUpdateTitleRequest
{
  public Guid id { get; set; }
  public string Title { get; set; }
}
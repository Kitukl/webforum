using Core.Entities;

namespace Core.Requests;

public class CourseRequest
{
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public Guid Creator { get; set; }
  public List<string> Categories { get; set; } = new();
}
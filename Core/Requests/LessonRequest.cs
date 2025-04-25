namespace Core.Requests;

public class LessonRequest
{
  public string Title { get; set; } = string.Empty;
  public int Minutes { get; set; }
  public Guid CourseId { get; set; }
}
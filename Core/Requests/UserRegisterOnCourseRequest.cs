namespace Core.Requests;

public class UserRegisterOnCourseRequest
{
  public Guid CourseId { get; set; }
  public Guid UserId { get; set; }
}
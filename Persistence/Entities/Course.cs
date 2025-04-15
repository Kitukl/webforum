namespace Persistence.Entities;

public class Course
{
  public Guid Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public User? Creator { get; set; }
  public List<Lesson> Lessons { get; set; } = new List<Lesson>();
  public List<User> Students { get; set; } = new();
  public List<string> Categories { get; set; } = new();
}
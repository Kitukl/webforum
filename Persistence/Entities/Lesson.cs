namespace Persistence.Entities;

public class Lesson
{
  private int _minutes;
  public Guid Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public int Minutes
  {
    get => _minutes;
    set
    {
      if (value < 0) throw new Exception("Хвилини менші за 0");
      _minutes = value;
    }
  }

  public Course? Course { get; set; }
}
using System.Globalization;

namespace Core.Entities;

public class User
{

  public User(string username, string password, string role)
  {
    Id = Guid.NewGuid();
    Username = username;
    Password = password;
    Role = role;
    CreatedCourses = new();
    EnrolledCourses = new();
    Discussions = new();
    Rating = 5;
  }
  
  private double _rating;
  public Guid Id { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
  public List<Course> CreatedCourses { get; set; } = new ();
  public List<Course> EnrolledCourses { get; set; } = new ();
  public List<Discussion> Discussions { get; set; } = new ();
  public double Rating
  {
    get => _rating;
    set
    {
      if (value > 5 && value < 0)
      {
        throw new Exception("Рейтинг повинен бути більшим за 0 і меншим за 5");
      }
      _rating = value;
    }
  }
}
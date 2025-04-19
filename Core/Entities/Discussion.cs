namespace Core.Entities;

public class Discussion
{
  public Discussion(string title, string content, User creator, List<string> categories)
  {
    Id = Guid.NewGuid();
    Title = title;
    Content = content;
    Creator = creator;
    Comments = new();
    Categories = categories;
  }

  private Discussion(){}
  public Guid Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public User? Creator { get; set; }
  public List<Comment> Comments { get; set; }
  public List<string> Categories { get; set; }
}
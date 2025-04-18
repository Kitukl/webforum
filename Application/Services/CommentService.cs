using Persistence.Contracts;

namespace Application.Services;

public class CommentService
{
  private readonly ICommentRepositories _commentRepositories;

  public CommentService(ICommentRepositories commentRepositories)
  {
    _commentRepositories = commentRepositories;
  }
}
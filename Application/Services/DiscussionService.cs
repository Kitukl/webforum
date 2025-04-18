using Persistence.Contracts;

namespace Application.Services;

public class DiscussionService
{
  private readonly IDiscussionRepositories _discussionRepositories;

  public DiscussionService(IDiscussionRepositories discussionRepositories)
  {
    _discussionRepositories = discussionRepositories;
  }
}
using Core.Entities;
using Persistence.Contracts;

namespace Application.Services;

public class DiscussionService
{
  private readonly IDiscussionRepositories _discussionRepositories;

  public DiscussionService(IDiscussionRepositories discussionRepositories)
  {
    _discussionRepositories = discussionRepositories;
  }


  public async Task Add(string title, string content, Guid creator, List<string> categories)
  {
    await _discussionRepositories.Add(title, content, creator, categories);
  }

  public async Task<Discussion> GetById(Guid id)
  {
     return await _discussionRepositories.GetById(id);
  }

  public async Task<List<Discussion>> Get()
  {
    return await _discussionRepositories.Get();
  }

  public async Task<List<Discussion>> GetByUser(Guid userId)
  {
    return await _discussionRepositories.GetByUser(userId);
  }

  public async Task<Discussion> GetByTitle(string title)
  {
    return await _discussionRepositories.GetByTitle(title);
  }

  public async Task UpdateTitle(Guid id, string title)
  {
    await _discussionRepositories.UpdateTitle(id, title);
  }

  public async Task UpdateContent(Guid id, string content)
  {
    await _discussionRepositories.UpdateContent(id, content);
  }

  public async Task Update(Guid id, List<Comment> comments)
  {
    await _discussionRepositories.Update(id, comments);
  }

  public async Task Delete(Guid id)
  {
    await _discussionRepositories.Delete(id);
  }
}
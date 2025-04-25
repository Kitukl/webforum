using Core.Entities;
using Persistence.Contracts;

namespace Application.Services;

public class CommentService
{
  private readonly ICommentRepositories _commentRepositories;

  public CommentService(ICommentRepositories commentRepositories)
  {
    _commentRepositories = commentRepositories;
  }

  public async Task Add(Guid creator, Guid discussion, string Content)
  {
    await _commentRepositories.Add(creator, discussion, Content);
  }

  public async Task<Comment> GetById(Guid id)
  {
    return await _commentRepositories.GetById(id);
  }

  public async Task<List<Comment>> Get()
  {
    return await _commentRepositories.Get();
  }

  public async Task<List<Comment>> GetByUser(Guid lectorId)
  {
    return await _commentRepositories.GetByUser(lectorId);
  }

  public async Task<Comment> GetByTitle(string title)
  {
    return await _commentRepositories.GetByTitle(title);
  }

  public async Task Update(Guid id, string content)
  {
    await _commentRepositories.Update(id, content);
  }

  public async Task Delete(Guid id)
  {
    await _commentRepositories.Delete(id);
  }
}
using Core.Entities;

namespace Persistence.Contracts;

public interface ICommentRepositories
{
  public Task Add(Guid creator, Guid discussion, string Content);
  public Task<Comment> GetById(Guid id);
  public Task<List<Comment>> Get();
  public Task<List<Comment>> GetByUser(Guid lectorId);
  public Task<Comment> GetByTitle(string title);
  public Task<Comment> Update();
  public Task Delete(Guid id);
}
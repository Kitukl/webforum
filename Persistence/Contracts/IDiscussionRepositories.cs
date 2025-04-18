using Persistence.Entities;

namespace Persistence.Contracts;

public interface IDiscussionRepositories
{
  public Task Add(string title, string content, Guid creator, List<string> categories);
  public Task<Discussion> GetById(Guid id);
  public Task<List<Discussion>> Get();
  public Task<List<Discussion>> GetByUser(Guid lectorId);
  public Task<Discussion> GetByTitle(string title);
  public Task<Discussion> Update();
  public Task Delete(Guid id);
}
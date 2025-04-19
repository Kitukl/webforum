using Core.Entities;

namespace Persistence.Contracts;

public interface IDiscussionRepositories
{
  public Task Add(string title, string content, Guid creator, List<string> categories);
  public Task<Discussion> GetById(Guid id);
  public Task<List<Discussion>> Get();
  public Task<List<Discussion>> GetByUser(Guid userId);
  public Task<Discussion> GetByTitle(string title);
  public Task UpdateTitle(Guid id, string title);
  public Task UpdateContent(Guid id, string content);
  public Task Update(Guid id, List<Comment> comments);
  public Task Delete(Guid id);
}
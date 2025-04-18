using Persistence.Entities;

namespace Persistence.Contracts;

public interface IUserRepositories
{
  public Task Add(string username, string password, string role);
  public Task<User> GetById(Guid id);
  public Task<List<User>> Get();
  public Task<User> GetByUserName(string username);
  public Task<User> Update();
  public Task Delete(Guid id);
}
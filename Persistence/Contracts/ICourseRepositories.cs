using Persistence.Entities;

namespace Persistence.Contracts;

public interface ICourseRepositories
{
  public Task Add(string title, string description,Guid creator, List<Guid> students, List<Guid> lessons, List<string> categories);
  public Task<Course> GetById(Guid id);
  public Task<List<Course>> Get();
  public Task<List<Course>> GetByLector(Guid lectorId);
  public Task<Course> GetByTitle(string title);
  public Task<Course> Update();
  public Task Delete(Guid id);
}
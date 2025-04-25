using Core.Entities;

namespace Persistence.Contracts;

public interface ICourseRepositories
{
  public Task Add(string title, string description,Guid creator, List<string> categories);
  public Task<Course> GetById(Guid id);
  public Task<List<Course>> Get();
  public Task<List<Course>> GetByLector(Guid lectorId);
  public Task<Course> GetByTitle(string title);
  public Task UpdateTitle(Guid id, string title);
  public Task UpdateDescription(Guid id, string description);
  
  public Task UpdateCategories(Guid id, List<string> categories);
  public Task Delete(Guid id);
}
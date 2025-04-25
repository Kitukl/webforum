using Core.Entities;
using Persistence.Contracts;

namespace Application.Services;

public class CourseService
{
  private readonly ICourseRepositories _courseRepositories;

  public CourseService(ICourseRepositories courseRepositories)
  {
    _courseRepositories = courseRepositories;
  }

  public async Task Add(string title, string description, Guid creator, List<string> categories)
  {
    await _courseRepositories.Add(title, description, creator, categories);
  }

  public async Task<Course> GetById(Guid id)
  {
    return await _courseRepositories.GetById(id);
  }

  public async Task<List<Course>> Get()
  { 
    return await _courseRepositories.Get();
  }

  public async Task<List<Course>> GetByLector(Guid lectorId)
  {
    return await _courseRepositories.GetByLector(lectorId);
  }

  public async Task<Course> GetByTitle(string title)
  {
    return await _courseRepositories.GetByTitle(title);
  }

  public async Task UpdateTitle(Guid id, string title)
  {
    await _courseRepositories.UpdateTitle(id,title);
  }

  public async Task UpdateDescription(Guid id, string description)
  {
    await _courseRepositories.UpdateDescription(id, description);
  }

  public async Task UpdateCategories(Guid id, List<string> categories)
  {
    await _courseRepositories.UpdateCategories(id, categories);
  }

  public async Task Delete(Guid id)
  {
    await _courseRepositories.Delete(id);
  }
}
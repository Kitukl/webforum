using Persistence.Contracts;
using Core.Entities;

namespace Persistence.Repositiries;

public class CoursesReposotories : ICourseRepositories
{
  private readonly Context _context;
  public CoursesReposotories(Context context)
  {
    _context = context;
  }
  public Task Add(string title, string description,Guid creator, List<Guid> students, List<Guid> lessons, List<string> categories)
  {
    throw new NotImplementedException();
  }

  public Task<Course> GetById(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<List<Course>> Get()
  {
    throw new NotImplementedException();
  }

  public Task<List<Course>> GetByLector(Guid lectorId)
  {
    throw new NotImplementedException();
  }

  public Task<Course> GetByTitle(string title)
  {
    throw new NotImplementedException();
  }

  public Task<Course> Update()
  {
    throw new NotImplementedException();
  }

  public Task Delete(Guid id)
  {
    throw new NotImplementedException();
  }
}
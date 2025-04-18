using Persistence.Contracts;
using Persistence.Entities;

namespace Persistence.Repositiries;

public class LessonsRepositories : ILessonRepositories
{
  private readonly Context _context;
  
  public LessonsRepositories(Context context)
  {
    _context = context;
  }
  
  public Task Add(string title, int minutes, Guid courseId)
  {
    throw new NotImplementedException();
  }

  public Task<List<Lesson>> Get()
  {
    throw new NotImplementedException();
  }

  public Task<Lesson> GetById(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<Lesson> GetByTitle(string title)
  {
    throw new NotImplementedException();
  }

  public Task Delete(Guid id)
  {
    throw new NotImplementedException();
  }
}
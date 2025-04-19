using Core.Entities;

namespace Persistence.Contracts;

public interface ILessonRepositories
{
  public Task Add(string title, int minutes, Guid courseId);
  public Task<List<Lesson>> Get();
  public Task<Lesson> GetById(Guid id);
  public Task<Lesson> GetByTitle(string title);
  public Task Delete(Guid id);
  
}
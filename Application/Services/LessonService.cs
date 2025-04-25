using Core.Entities;
using Persistence.Contracts;

namespace Application.Services;

public class LessonService
{
  private readonly ILessonRepositories _lessonRepositories;

  public LessonService(ILessonRepositories lessonRepositories)
  {
    _lessonRepositories = lessonRepositories;
  }
  
  public async Task Add(string title, int minutes, Guid courseId)
  {
    await _lessonRepositories.Add(title, minutes, courseId);
  }

  public async Task<Lesson> GetById(Guid id)
  {
    return await _lessonRepositories.GetById(id);
  }

  public async Task<Lesson> GetByTitle(string title)
  {
    return await _lessonRepositories.GetByTitle(title);
  }

  public async Task Delete(Guid id)
  {
    await _lessonRepositories.Delete(id);
  }
}
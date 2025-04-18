using Persistence.Contracts;

namespace Application.Services;

public class LessonService
{
  private readonly ILessonRepositories _lessonRepositories;

  public LessonService(ILessonRepositories lessonRepositories)
  {
    _lessonRepositories = lessonRepositories;
  }
}
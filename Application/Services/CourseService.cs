using Persistence.Contracts;

namespace Application.Services;

public class CourseService
{
  private readonly ICourseRepositories _courseRepositories;

  public CourseService(ICourseRepositories courseRepositories)
  {
    _courseRepositories = courseRepositories;
  }
}
using Persistence.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositiries;

public class LessonsRepositories : ILessonRepositories
{
  private readonly Context _context;
  public LessonsRepositories(Context context)
  {
    _context = context;
  }
  
  public async Task Add(string title, int minutes, Guid courseId)
  {
    var lesson = new Lesson()
    {
      Id = Guid.NewGuid(),
      Title = title,
      Minutes = minutes,
      Course = await _context.Courses
        .FirstOrDefaultAsync(c => c.Id == courseId)
    };
    await _context.Lessons
      .AddAsync(lesson);
    await _context.SaveChangesAsync();
  }

  public async Task<Lesson> GetById(Guid id)
  {
    return await _context.Lessons
      .FirstOrDefaultAsync(l => l.Id == id) ?? throw new Exception("Lesson not found");
  }

  public async Task<Lesson> GetByTitle(string title)
  {
   return await _context.Lessons
      .FirstOrDefaultAsync(l => l.Title == title) ?? throw new Exception("Lesson not found");
  }

  public async Task Delete(Guid id)
  {
    await _context.Lessons
      .Where(l => l.Id == id)
      .ExecuteDeleteAsync();
    await _context.SaveChangesAsync();
  }
}
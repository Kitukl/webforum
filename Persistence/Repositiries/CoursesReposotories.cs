using Persistence.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositiries;

public class CoursesReposotories : ICourseRepositories
{
  private readonly Context _context;
  public CoursesReposotories(Context context)
  {
    _context = context;
  }
  public async Task Add(string title, string description,Guid creator, List<string> categories)
  {
    var course = new Course()
    {
      Id = Guid.NewGuid(),
      Title = title,
      Description = description,
      Creator = await _context.Users
        .FirstOrDefaultAsync(u => u.Id == creator),
      Students = new(),
      Lessons = new(),
      Categories = categories
    };

    await _context.Courses
      .AddAsync(course);
    await _context.SaveChangesAsync();
  }

  public async Task<Course> GetById(Guid id)
  {
    return await _context.Courses
      .FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception("Course not found");
  }

  public async Task<List<Course>> Get()
  {
    return await _context.Courses
      .Include(c => c.Lessons)
      .Include(c => c.Creator)
      .Include(c => c.Students)
      .ToListAsync();
  }

  public async Task<List<Course>> GetByLector(Guid lectorId)
  {
   return await _context.Courses
     .Include(c => c.Lessons)
     .Include(c => c.Creator)
     .Include(c => c.Students)
      .Where(c => c.Creator.Id == lectorId)
      .ToListAsync();
  }

  public async Task<Course> GetByTitle(string title)
  {
    return await _context.Courses
      .Include(c => c.Creator)
      .Include(c => c.Lessons)
      .Include(c => c.Students)
      .FirstOrDefaultAsync(c => c.Title == title) ?? throw new Exception("Course not found");
  }

  public async Task UpdateTitle(Guid id, string title)
  {
    await _context.Courses
      .Where(c => c.Id == id)
      .ExecuteUpdateAsync(s => s.SetProperty(c => c.Title, title));
    await _context.SaveChangesAsync();
  }

  public async Task UpdateDescription(Guid id, string description)
  {
    await _context.Courses
      .Where(c => c.Id == id)
      .ExecuteUpdateAsync(s => s.SetProperty(c => c.Description, description));
    await _context.SaveChangesAsync();
  }

  public async Task UpdateCategories(Guid id, List<string> categories)
  {
    await _context.Courses
      .Where(c => c.Id == id)
      .ExecuteUpdateAsync(s => s.SetProperty(c => c.Categories, categories));
    await _context.SaveChangesAsync();
    
  }

  public async Task Delete(Guid id)
  {
    await _context.Courses
      .Where(c => c.Id == id)
      .ExecuteDeleteAsync();
    await _context.SaveChangesAsync();
  }
}
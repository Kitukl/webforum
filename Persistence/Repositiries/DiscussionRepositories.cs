using Persistence.Contracts;
using Core.Entities;
using Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositiries;

public class DiscussionRepositories : IDiscussionRepositories
{
  private readonly Context _context;
  public DiscussionRepositories(Context context)
  {
    _context = context;
  }
  public async Task Add(string title, string content, Guid creator, List<string> categories)
  {
    var user = await _context.Users
      .FirstOrDefaultAsync(u => u.Id == creator) ?? throw new Exception("Not found user");
    var discussion = new Discussion(title, content, user, categories);
    await _context.Discussions.AddAsync(discussion);
    await _context.SaveChangesAsync();
  }

  public async Task<Discussion> GetById(Guid id)
  {
    return await _context.Discussions
             .Include(d => d.Creator)
             .Include(d => d.Comments)
             .FirstOrDefaultAsync(d => d.Id == id) ??
           throw new Exception("Not found discussion with this id");
  }

  public async Task<List<Discussion>> Get()
  {
    return await _context.Discussions
      .Include(d => d.Creator)
      .Include(d => d.Comments)
      .ToListAsync();
  }

  public async Task<List<Discussion>> GetByUser(Guid userId)
  {
    return await _context.Discussions
      .Where(d => d.Creator.Id == userId)
      .Include(d => d.Creator)
      .Include(d => d.Comments)
      .ToListAsync();
  }

  public async Task<Discussion> GetByTitle(string title)
  {
    return await _context.Discussions
      .Include(d => d.Creator)
      .Include(d => d.Comments)
      .FirstOrDefaultAsync(d => d.Title == title) ?? throw new Exception("No discussion with this title");
  }

  public async Task UpdateTitle(Guid id, string title)
  {
    await _context.Discussions
      .Where(d => d.Id == id)
      .ExecuteUpdateAsync(s => s.SetProperty(d => d.Title, title));
    await _context.SaveChangesAsync();
  }

  public async Task UpdateContent(Guid id, string content)
  {
    await _context.Discussions
      .Where(d => d.Id == id)
      .ExecuteUpdateAsync(s => s.SetProperty(d => d.Content, content));
    await _context.SaveChangesAsync();
  }

  public async Task Update(Guid id, List<Comment> comments)
  {
    await _context.Discussions
      .Where(d => d.Id == id)
      .ExecuteUpdateAsync(s => s.SetProperty(d => d.Comments, comments));
    await _context.SaveChangesAsync();
  }

  public async Task Delete(Guid id)
  {
    await _context.Discussions
      .Where(d => d.Id == id)
      .ExecuteDeleteAsync();
    await _context.SaveChangesAsync();
  }
}
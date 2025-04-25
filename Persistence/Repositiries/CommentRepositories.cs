using Persistence.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositiries;

public class CommentRepositories : ICommentRepositories
{
  private readonly Context _context;

  public CommentRepositories(Context context)
  {
    _context = context;
  }
  public async Task Add(Guid creator, Guid discussion, string content)
  {
    var comment = new Comment()
    {
      Id = Guid.NewGuid(),
      Content = content,
      Creator = await _context.Users
        .FirstOrDefaultAsync(u => u.Id == creator),
      CreatedAt = DateTime.Now.ToUniversalTime(),
      Discussion = await _context.Discussions
        .FirstOrDefaultAsync(d => d.Id == discussion) ?? throw new Exception("Comment didin`t create")
    };
    await _context.Comments.AddAsync(comment);
    await _context.SaveChangesAsync();
  }

  public async Task<Comment> GetById(Guid id)
  {
    var comment = await _context.Comments
      .Include(c => c.Creator)
      .Include(c => c.Discussion)
      .FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception("Comment not found");
    return comment;
  }

  public async Task<List<Comment>> Get()
  {
    var comments = await _context.Comments
      .Include(c => c.Creator)
      .Include(c => c.Discussion)
      .ToListAsync();
    return comments;
  }

  public async Task<List<Comment>> GetByUser(Guid lectorId)
  {
    var comments = await _context.Comments
      .Include(c => c.Creator)
      .Include(c => c.Discussion)
      .Where(c => c.Creator.Id == lectorId)
      .ToListAsync();

    return comments;
  }

  public async Task<Comment> GetByTitle(string title)
  {
    var comment = await _context.Comments
      .Include(c => c.Creator)
      .Include(c => c.Discussion)
      .FirstOrDefaultAsync(c => c.Content == title) ?? throw new Exception("comment not found");
    return comment;
  }

  public async Task Update(Guid id, string content)
  {
     await _context.Comments
      .Where(c => c.Id == id)
      .ExecuteUpdateAsync(s => s.SetProperty(
        c => c.Content, content
        ).SetProperty(
        c => c.CreatedAt, DateTime.Now.ToUniversalTime()
        ));
    await _context.SaveChangesAsync();
  }

  public async Task Delete(Guid id)
  {
    await _context.Comments
      .Where(c => c.Id == id)
      .ExecuteDeleteAsync();
    await _context.SaveChangesAsync();
  }
}
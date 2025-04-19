using Persistence.Contracts;
using Core.Entities;

namespace Persistence.Repositiries;

public class CommentRepositories : ICommentRepositories
{
  private readonly Context _context;

  public CommentRepositories(Context context)
  {
    _context = context;
  }
  public Task Add(Guid creator, Guid discussion, string Content)
  {
    throw new NotImplementedException();
  }

  public Task<Comment> GetById(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<List<Comment>> Get()
  {
    throw new NotImplementedException();
  }

  public Task<List<Comment>> GetByUser(Guid lectorId)
  {
    throw new NotImplementedException();
  }

  public Task<Comment> GetByTitle(string title)
  {
    throw new NotImplementedException();
  }

  public Task<Comment> Update()
  {
    throw new NotImplementedException();
  }

  public Task Delete(Guid id)
  {
    throw new NotImplementedException();
  }
}
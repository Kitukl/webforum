using Persistence.Contracts;
using Persistence.Entities;

namespace Persistence.Repositiries;

public class DiscussionRepositories : IDiscussionRepositories
{
  private readonly Context _context;
  public DiscussionRepositories(Context context)
  {
    _context = context;
  }
  public Task Add(string title, string content, Guid creator, List<string> categories)
  {
    throw new NotImplementedException();
  }

  public Task<Discussion> GetById(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<List<Discussion>> Get()
  {
    throw new NotImplementedException();
  }

  public Task<List<Discussion>> GetByUser(Guid lectorId)
  {
    throw new NotImplementedException();
  }

  public Task<Discussion> GetByTitle(string title)
  {
    throw new NotImplementedException();
  }

  public Task<Discussion> Update()
  {
    throw new NotImplementedException();
  }

  public Task Delete(Guid id)
  {
    throw new NotImplementedException();
  }
}
using Infrastructure.Hasher;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;
using Persistence.Entities;

namespace Persistence.Repositiries;

public class UserRepositories : IUserRepositories
{
  private readonly Context _context;
  
  public UserRepositories(Context context)
  {
    _context = context;
  }
  
  public async Task Add(string username, string password, string role)
  {
    var user = new User(username, password, role);
    await _context.Users.AddAsync(user);
    await _context.SaveChangesAsync();
  }

  public async Task<User> GetById(Guid id)
  {
    return await _context.Users
      .FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");
  }

  public async Task<List<User>> Get()
  {
    return await _context.Users.ToListAsync();
  }

  public async Task<User> GetByUserName(string username)
  {
    return await _context.Users
      .FirstOrDefaultAsync(u => u.Username == username) ?? throw new Exception("User not found");
  }

  public async Task UpdateUsername(Guid id, string username)
  {
    await _context.Users
      .Where(u => u.Id == id)
      .ExecuteUpdateAsync(s => s
        .SetProperty(u => u.Username, username));

    await _context.SaveChangesAsync();
  }
  
  public async Task UpdatePassword(Guid id, string password)
  {
    await _context.Users
      .Where(u => u.Id == id)
      .ExecuteUpdateAsync(s => s
        .SetProperty(u => u.Password, password));

    await _context.SaveChangesAsync();
  }

  public async Task Delete(Guid id)
  {
    await _context.Users
      .Where(u => u.Id == id)
      .ExecuteDeleteAsync();

    await _context.SaveChangesAsync();
  }
}
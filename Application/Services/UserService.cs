using Infrastructure.Hasher;
using Persistence.Contracts;
using Core.Entities;

namespace Application.Services;

public class UserService
{
  private readonly IUserRepositories _userRepositories;
  private readonly IJWTProvider _jwtProvider;

  public UserService(IUserRepositories userRepositories, IJWTProvider jwtProvider)
  {
    _userRepositories = userRepositories;
    _jwtProvider = jwtProvider;
  }

  public async Task Register(string username, string password, string role)
  {
    var hashedPassword = PasswordHasher.HashPassword(password);
    await _userRepositories.Add(username, hashedPassword, role);
  }

  public async Task<string> Login(string username, string password)
  {
    var user = await _userRepositories.GetByUserName(username);
    if (!PasswordHasher.VerifyPassword(password, user.Password)) return "Incorrect password";
    var token = _jwtProvider.GenerateToken(user);
    return token;
  }

  public async Task<User> GetById(Guid id)
  {
    var user = await _userRepositories.GetById(id);
    return user;
  }

  public async Task<User> GetByUsername(string username)
  {
    var user = await _userRepositories.GetByUserName(username);
    return user;
  }

  public async Task UpdateUsername(Guid id, string username)
  {
    await _userRepositories.UpdateUsername(id,username);
  }
  
  public async Task UpdatePassword(Guid id, string password)
  {
    var hashedPassword = PasswordHasher.HashPassword(password);
    await _userRepositories.UpdateUsername(id, hashedPassword);
  }
  
  public async Task<List<User>> Get()
  {
    var users = await _userRepositories.Get();
    return users;
  }

  public async Task Delete(Guid id)
  {
    await _userRepositories.Delete(id);
  }
}
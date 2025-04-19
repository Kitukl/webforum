using Core.Entities;

namespace Application;

public interface IJWTProvider
{
  public string GenerateToken(User user);
}
namespace Infrastructure.Hasher;

static public class PasswordHasher
{
  static public string HashPassword(string password)
  {
    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
    return hashedPassword;
  }

  static public bool VerifyPassword(string password, string hashedPassword)
  {
    var isEquel = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    return isEquel;
  }
}
namespace Infrastructure;

public class JWTOptions
{
  public string SecretKey { get; set; }
  public int Expires { get; set; }
}
namespace LearnMS.API.Security.JwtBearer;

public class JwtBearerConfig
{
    public const string Section = "JwtBearer";

    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public int TokenExpirationInMinutes { get; set; }
}
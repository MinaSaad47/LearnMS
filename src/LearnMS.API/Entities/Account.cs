using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnMS.API.Entities;


[JsonConverter(typeof(StringEnumConverter))]
public enum ProviderType
{
    Google,
    Local
}

public class Account
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public string? PasswordHash { get; set; }
    public required ProviderType ProviderType { get; set; }
    public string? ProviderId { get; set; }
    public string? ProfilePicture { get; set; }

    public string? VerificationToken { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpiresAt { get; set; }

    public User User = null!;
}
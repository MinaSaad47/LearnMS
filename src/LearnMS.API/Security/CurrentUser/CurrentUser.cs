using LearnMS.API.Entities;

namespace LearnMS.API.Security;

public sealed class CurrentUser
{
    public Guid Id { get; init; }
    public UserRole Role { get; init; }
    public IEnumerable<Permission> Permissions { get; init; } = new List<Permission>();
}
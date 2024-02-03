using LearnMS.API.Entities;

namespace LearnMS.API.Features.Profile.Contracts;

public sealed record GetProfileQuery
{
    public required Guid Id { get; init; }
}

public sealed record GetProfileResult
{
    public Account? Account { get; init; }
}

public sealed record GetProfileResponse
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public string? Name { get; init; }
    public string? ProfilePicture { get; init; }
    public Permission[]? Permissions { get; init; }
    public required string Role { get; init; }
    public decimal? Credits { get; init; }
}
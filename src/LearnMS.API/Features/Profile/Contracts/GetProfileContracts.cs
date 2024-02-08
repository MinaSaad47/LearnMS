using LearnMS.API.Entities;

namespace LearnMS.API.Features.Profile.Contracts;

public sealed record GetProfileQuery
{
    public required Guid Id { get; init; }
}

public sealed record GetProfileResult
{
    public Account? Account { get; init; }
    public string? FullName { get; init; }
    public string? PhoneNumber { get; init; }
    public string? ParentPhoneNumber { get; init; }
    public string? School { get; init; }
    public StudentLevel? Level { get; init; }

}

public sealed record GetProfileResponse
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public string? FullName { get; init; }
    public string? PhoneNumber { get; init; }
    public string? ParentPhoneNumber { get; init; }
    public string? School { get; init; }
    public StudentLevel? Level { get; init; }
    public string? ProfilePicture { get; init; }
    public Permission[]? Permissions { get; init; }
    public required string Role { get; init; }
    public decimal? Credits { get; init; }
}
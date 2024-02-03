using LearnMS.API.Entities;

namespace LearnMS.API.Features.Profile.Contracts;

public sealed record UpdateStudentProfileRequest
{
    public string? FullName { get; init; }
    public string? ProfilePicture { get; init; }

    public string? PhoneNumber { get; init; }
    public string? ParentPhoneNumber { get; init; }
    public string? SchoolName { get; init; }
    public StudentLevel? Level { get; init; }
}

public sealed record UpdateStudentProfileCommand
{
    public required Guid Id { get; init; }
    public string? FullName { get; init; }
    public string? ProfilePicture { get; init; }

    public string? PhoneNumber { get; init; }
    public string? ParentPhoneNumber { get; init; }
    public string? SchoolName { get; init; }
    public StudentLevel? Level { get; init; }
}
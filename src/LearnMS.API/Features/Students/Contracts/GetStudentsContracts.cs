using LearnMS.API.Entities;

namespace LearnMS.API.Features.Students.Contracts;

public sealed record GetStudentsQuery
{
    public string? Search { get; init; }
    public int? Page { get; init; } = 0;
    public int? PageSize { get; init; } = 10;
}


public sealed record SingleStudent
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string? ProfilePicture { get; init; }
    public required string FullName { get; init; }
    public required string PhoneNumber { get; init; }
    public required string ParentPhoneNumber { get; init; }
    public required string StudentCode { get; init; }

    public required bool IsVerified {get; init;}
    public required string SchoolName { get; init; }
    public required decimal Credit { get; init; }
    public required StudentLevel Level { get; init; }
}
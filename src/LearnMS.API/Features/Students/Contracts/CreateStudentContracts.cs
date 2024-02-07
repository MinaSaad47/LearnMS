using LearnMS.API.Entities;

namespace LearnMS.API.Features.Students.Contracts;

public sealed record CreateStudentCommand
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string School { get; init; }
    public required string FullName { get; init; }
    public required string PhoneNumber { get; init; }
    public required string ParentPhoneNumber { get; init; }
    public required StudentLevel Level { get; init; }
}

public sealed record CreateStudentRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string School { get; init; }
    public required string FullName { get; init; }
    public required string PhoneNumber { get; init; }
    public required string ParentPhoneNumber { get; init; }
    public required StudentLevel Level { get; init; }
}
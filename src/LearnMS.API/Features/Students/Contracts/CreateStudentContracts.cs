using System.ComponentModel.DataAnnotations;
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
    public required string StudentCode { get; init; }

    public required StudentLevel Level { get; init; }
}

public sealed record CreateStudentRequest
{
    [Required, EmailAddress]
    public required string Email { get; init; }
    [Required, MinLength(8)]
    public required string Password { get; init; }
    [Required, MinLength(3)]
    public required string School { get; init; }
    [Required, MinLength(3)]
    public required string FullName { get; init; }
    [Required, Length(11, 11)]
    public required string PhoneNumber { get; init; }
    [Required, Length(11, 11)]
    public required string ParentPhoneNumber { get; init; }
    [Required,Length(1, 8)]
    public required string StudentCode { get; init; }
    [Required]
    public required StudentLevel Level { get; init; }
}
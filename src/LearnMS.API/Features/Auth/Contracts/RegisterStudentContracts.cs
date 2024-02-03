using LearnMS.API.Entities;

namespace LearnMS.API.Features.Auth;

public sealed record RegisterStudentRequest(
    string Email,
    string Password,
    string School,
    string FullName,
    string PhoneNumber,
    string ParentPhoneNumber,
    StudentLevel Level
);

public sealed record RegisterStudentExternalRequest(
    string Token,
    string Provider
);

public sealed record RegisterStudentCommand(
    string Email,
    string Password,
    string School,
    string FullName,
    string PhoneNumber,
    string ParentPhoneNumber,
    StudentLevel Level
);



public sealed record RegisterStudentExternalCommand
{
    public required string Token { get; init; }
    public required string Provider { get; init; }

}
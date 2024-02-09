using System.ComponentModel.DataAnnotations;
using LearnMS.API.Entities;
namespace LearnMS.API.Features.Auth;

public sealed record RegisterStudentRequest(

    [Required]
    [EmailAddress]
    string Email,
    [Required]
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

public sealed record RegisterStudentCommand
{
    public required string Email;
    public required string Password;
    public required string School;
    public required string FullName;
    public required string PhoneNumber;
    public required string ParentPhoneNumber;
    public required StudentLevel Level;
};



public sealed record RegisterStudentExternalCommand
{
    public required string Token { get; init; }
    public required string Provider { get; init; }

}
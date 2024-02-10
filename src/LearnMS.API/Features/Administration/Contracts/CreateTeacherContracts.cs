using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Administration.Contracts;


public sealed record CreateTeacherCommand
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public sealed record CreateTeacherRequest
{
    [Required, EmailAddress]
    public required string Email { get; set; }
    [Required, MinLength(8)]
    public required string Password { get; set; }
}
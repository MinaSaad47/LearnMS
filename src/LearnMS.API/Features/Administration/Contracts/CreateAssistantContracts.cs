using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Administration.Contracts;


public sealed record CreateAssistantCommand
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public sealed record CreateAssistantRequest
{
    [Required, EmailAddress]
    public required string Email { get; set; }
    [Required, MinLength(8)]
    public required string Password { get; set; }
}
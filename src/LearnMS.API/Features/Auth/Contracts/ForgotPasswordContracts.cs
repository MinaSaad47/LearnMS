using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Auth.Contracts;

public sealed record ForgotPasswordRequest
{
    [Required, EmailAddress]
    public required string Email { get; init; }
};

public sealed record ForgotPasswordCommand
{
    public required string Email { get; init; }
};

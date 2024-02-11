using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Auth.Contracts;


public sealed record ResetPasswordRequest
{
    [Required, MinLength(8)]
    public required string Password { get; init; }
    [Required]
    public required string Token { get; init; }
}

public sealed record ResetPasswordCommand
{
    public required string Password { get; init; }
    public required string Token { get; init; }
}
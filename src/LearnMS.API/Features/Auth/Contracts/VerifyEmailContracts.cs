using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Auth.Contracts;

public sealed record VerifyEmailCommand
{
    [Required]
    public required string Token { get; init; }
}
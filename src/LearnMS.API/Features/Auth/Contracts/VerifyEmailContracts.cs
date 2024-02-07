namespace LearnMS.API.Features.Auth.Contracts;

public sealed record VerifyEmailCommand
{
    public required string Token { get; init; }
}
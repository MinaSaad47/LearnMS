using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Auth;

public sealed record RegisterAssistantRequest(
    [Required, EmailAddress]
    string Email,
    [Required, MinLength(8)]
    string Password
);

public sealed record RegisterAssistantCommand(
    string Email,
    string Password
);


public sealed record RegisterResult(Guid Id);
public sealed record RegisterResponse(Guid Id);
using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Auth.Contracts;

public sealed record LoginRequest(
        [Required, EmailAddress]
    string Email,
        [Required, MinLength(8)]
    string Password
);

public sealed record LoginExternalRequest(
    string Token,
    string Provider
);

public sealed record LoginCommand(
    string Email,
    string Password
);


public sealed record LoginExternalCommand(
    string Token,
    string Provider
);


public sealed record LoginResult(Guid Id, string Token);

public sealed record LoginResponse(Guid Id, string Token);
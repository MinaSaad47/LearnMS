namespace LearnMS.API.Features.Auth;

public sealed record RegisterStudentRequest(
    string Email,
    string Password
);

public sealed record RegisterStudentExternalRequest(
    string Token,
    string Provider
);

public sealed record RegisterStudentCommand(
    string Email,
    string Password
);



public sealed record RegisterStudentExternalCommand
{
    public required string Token { get; init; }
    public required string Provider { get; init; }

}
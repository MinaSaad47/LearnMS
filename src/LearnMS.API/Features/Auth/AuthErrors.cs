using LearnMS.API.Common;

namespace LearnMS.API.Features.Auth;

public abstract class AuthErrors
{
    public static readonly ApiError InvalidCredentials = new("auth/invalid-credentials", "Invalid credentials", StatusCodes.Status401Unauthorized);
    public static readonly ApiError EmailAlreadyExists = new("auth/email-already-exists", "Email already exists", StatusCodes.Status409Conflict);
    public static readonly ApiError Unauthorized = new("auth/unauthorized", "Unauthorized", StatusCodes.Status401Unauthorized);
    public static readonly ApiError Forbidden = new("auth/forbidden", "Forbidden", StatusCodes.Status403Forbidden);
}
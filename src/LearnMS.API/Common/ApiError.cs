
namespace LearnMS.API.Common;

public class ApiException(ApiError error) : Exception
{
    public ApiError Error => error;
}

public static class ServerErrors
{

    public static readonly ApiError Internal = new(
        "server/internal",
        "An internal server error has occurred. Please try again later.",
        StatusCodes.Status500InternalServerError
    );

    public static readonly ApiError NotImplemented = new(
        "server/not-implemented",
        "The requested functionality is not implemented.",
        StatusCodes.Status501NotImplemented
    );
}



public record ApiError(
    string Code,
    string Message,
    int StatusCode = StatusCodes.Status501NotImplemented
)
{
};
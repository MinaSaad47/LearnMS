namespace LearnMS.API.Common;

public static class ApiWrapper
{
    public class Success<TData>
    {
        public bool Status { get; } = true;
        public string? Message { get; init; }
        public TData? Data { get; init; }
    }

    public class Failure
    {
        public bool Status { get; } = false;
        public string? Message { get; init; }
        public required string Code { get; init; }
    }
}
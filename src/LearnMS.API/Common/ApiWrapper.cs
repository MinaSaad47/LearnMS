using Microsoft.AspNetCore.Mvc;
using Serilog;

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

        public static IActionResult GenerateErrorResponse(ActionContext context)
        {
            Log.Debug("{@model}", context.ModelState);

            var apiError = new Failure
            {
                Code = $"validation/{context.ModelState.AsEnumerable().First().Key.ToLower()}",
                Message = context.ModelState.AsEnumerable().First().Value?.Errors.AsEnumerable().First().ErrorMessage
            };
            return new BadRequestObjectResult(apiError);
        }
    }
}
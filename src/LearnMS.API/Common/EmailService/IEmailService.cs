namespace LearnMS.API.Common.EmailService;

public interface IEmailService
{
    public Task SendAsync(SendEmailRequest request);
}

public sealed record SendEmailRequest
{
    public string? From { get; init; }
    public required string To { get; init; }
    public required string Subject { get; init; }
    public required string Body { get; init; }
}
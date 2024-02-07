namespace LearnMS.API.Common.EmailService;

public sealed class EmailConfig
{
    public const string Section = "Email";

    public required string Host { get; init; }
    public required int Port { get; init; }
    public required bool UseSsl { get; init; } = true;

    public required string Username { get; init; }
    public required string Password { get; init; }


    public required string Sender { get; init; }
}
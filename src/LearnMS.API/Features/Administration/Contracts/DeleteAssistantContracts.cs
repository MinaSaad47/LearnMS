namespace LearnMS.API.Features.Administration.Contracts;

public sealed record DeleteAssistantCommand
{
    public required Guid Id { get; init; }
}
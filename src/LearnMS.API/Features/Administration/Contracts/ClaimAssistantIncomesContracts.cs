namespace LearnMS.API.Features.Administration.Contracts;

public sealed record ClaimAssistantIncomesCommand
{
    public required Guid AssistantId { get; set; }
}
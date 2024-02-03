namespace LearnMS.API.Features.CreditCodes.Contracts;

public sealed record GenerateCreditCodesCommand(
    int Count,
    decimal Value,
    Guid? AssistantId = null
);
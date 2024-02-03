namespace LearnMS.API.Features.CreditCodes.Contracts;

public sealed record RedeemCreditCodeCommand
{
    public required string Code { get; init; }
    public required Guid StudentId { get; init; }
};

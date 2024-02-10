using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.CreditCodes.Contracts;

public sealed record RedeemCreditCodeRequest
{
    [Required]
    public required string Code { get; init; }
};

public sealed record RedeemCreditCodeCommand
{
    public required string Code { get; init; }
    public required Guid StudentId { get; init; }
};

public sealed record RedeemCreditCodeResult
{
    public required decimal Value { get; init; }
}

public sealed record RedeemCreditCodeResponse
{
    public required decimal Value { get; init; }
}
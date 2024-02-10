using System.ComponentModel.DataAnnotations;
using LearnMS.API.Entities;

namespace LearnMS.API.Features.CreditCodes.Contracts;

public record SellCreditCodesCommand
{
    public required List<string> Codes { get; init; }
}

public record SellCreditCodesRequest
{
    [Required]
    public required List<string> Codes { get; init; }
}

public record SellCreditCodesResult
{
    public required List<CreditCode> CreditCodes { get; init; }
}

public record SellCreditCodesResponse
{
    public required List<CreditCode> CreditCodes { get; init; }
}
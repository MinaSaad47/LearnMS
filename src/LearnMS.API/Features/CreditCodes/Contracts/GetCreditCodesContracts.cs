using LearnMS.API.Entities;

namespace LearnMS.API.Features.CreditCodes.Contracts;

public sealed record GetCreditCodesQuery
{
    public string? Search { get; init; }
    public Guid? GeneratorId { get; init; }
    public int? Page { get; init; } = 0;
    public int? PageSize { get; init; } = 10;
    public string? SortOrder { get; init; } = "asc";
};

public sealed record SingleCreditCodeItem
{
    public required string Code { get; init; }
    public required decimal Value { get; init; }
    public required CreditCodeStatus Status { get; init; }
    public required CreditCodeGenerator? Generator { get; init; }
    public required DateTime GeneratedAt { get; init; }
    public required DateTime? SoldAt { get; init; }
    public required CreditCodeSeller? Seller { get; init; }
    public required CreditCodeRedeemer? Redeemer { get; init; }
}

public sealed record CreditCodeGenerator
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
}

public sealed record CreditCodeRedeemer
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required DateTime RedeemedAt { get; init; }
}

public sealed record CreditCodeSeller
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
}
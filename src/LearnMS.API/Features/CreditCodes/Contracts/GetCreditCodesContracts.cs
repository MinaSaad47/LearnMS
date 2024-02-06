using LearnMS.API.Entities;

namespace LearnMS.API.Features.CreditCodes.Contracts;

public sealed record GetCreditCodesQuery
{
    public string? Search { get; init; }
    public int? Page { get; init; } = 0;
    public int? PageSize { get; init; } = 10;
    public string? SortOrder { get; init; } = "asc";
};

public sealed record SingleCreditCodeItem
{
    public required string Code { get; init; }
    public required decimal Value { get; init; }
    public required string Status { get; init; }
    public required CreditCodeRedeemer? Redeemer { get; init; }
    public required CreditCodeGenerator? Generator { get; init; }
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
}
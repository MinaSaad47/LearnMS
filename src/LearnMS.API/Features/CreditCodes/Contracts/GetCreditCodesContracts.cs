using LearnMS.API.Entities;

namespace LearnMS.API.Features.CreditCodes.Contracts;

public sealed record GetCreditCodesQuery();

public sealed record GetCreditCodesResult(
    List<CreditCode> Items
);
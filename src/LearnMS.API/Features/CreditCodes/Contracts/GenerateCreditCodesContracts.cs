using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.CreditCodes.Contracts;

public sealed record GenerateCreditCodesCommand
{

    public int Count;
    public decimal Value;
    public Guid? GeneratorId = null;
}

public sealed record GenerateCreditCodesRequest
{
    [Required, Range(1, 100)]
    public required int Count;
    [Required, Range(10, 1000)]
    public required decimal Value;
}
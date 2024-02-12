using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnMS.API.Features.Administration.Contracts;

public sealed record GetAssistantIncomeQuery
{
    public required Guid AssistantId { get; set; }
    public required int? Page { get; set; }
    public required int? PageSize { get; set; }
}

public sealed record GetAssistantIncomeResult
{
    public required decimal UnClaimedIncome { get; set; }
    public required decimal TotalIncome { get; set; }
    public required PageList<SingleAssistantIncome> Data { get; set; }
}

public sealed record SingleAssistantIncome
{
    public IncomeType Type
    {
        get
        {
            if (Code is not null) return IncomeType.CodeSold;
            return IncomeType.StudentCredit;
        }
    }

    public required decimal Amount { get; set; }
    public required string? Code { get; set; }
    public required Guid? StudentId { get; set; }
    public required DateTime HappenedAt { get; set; }
    public required DateTime? ClaimedAt { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum IncomeType
{
    CodeSold,
    StudentCredit
}


public sealed record GetAssistantIncomeResponse
{
    public required decimal UnClaimedIncome { get; set; }
    public required decimal TotalIncome { get; set; }
    public required PageList<SingleAssistantIncome> Data { get; set; }
}
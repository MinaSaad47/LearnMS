namespace LearnMS.API.Entities;

public sealed class CreditCode
{
    public required string Code { get; set; }
    public decimal Value { get; set; }
    public Guid? AssistantId { get; set; }
    public Guid? StudentId { get; set; }
    public CreditCodeStatus Status { get; set; } = CreditCodeStatus.Fresh;

}

public enum CreditCodeStatus
{
    Fresh,
    Sold,
    Redeemed
}
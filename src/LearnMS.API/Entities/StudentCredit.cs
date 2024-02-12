namespace LearnMS.API.Entities;

public class StudentCredit
{
    public Guid Id { get; set; }
    public required Guid? AssistantId { get; init; }
    public required Guid StudentId { get; init; }
    public required decimal Value { get; init; }
    public DateTime CreditedAt { get; init; } = DateTime.UtcNow;
    public DateTime? ClaimedAt { get; set; }
}
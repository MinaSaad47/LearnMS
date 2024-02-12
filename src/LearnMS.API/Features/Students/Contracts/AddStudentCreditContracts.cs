namespace LearnMS.API.Features.Students.Contracts;

public sealed record AddStudentCreditCommand
{
    public required Guid Id { get; init; }
    public Guid? AssistantId { get; init; }
    public required decimal Amount { get; init; }
}

public sealed record AddStudentCreditRequest
{
    public required decimal Amount { get; init; }
}
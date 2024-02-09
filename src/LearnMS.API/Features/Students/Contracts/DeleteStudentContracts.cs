namespace LearnMS.API.Features.Students.Contracts;

public sealed record DeleteStudentCommand
{
    public required Guid Id { get; init; }
}
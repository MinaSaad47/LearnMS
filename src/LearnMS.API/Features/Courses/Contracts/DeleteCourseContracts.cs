namespace LearnMS.API.Features.Courses.Contracts;

public sealed record DeleteCourseCommand
{
    public required Guid Id { get; init; }
}
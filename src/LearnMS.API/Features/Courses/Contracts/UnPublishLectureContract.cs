namespace LearnMS.API.Features.Courses.Contracts;


public sealed record UnPublishLectureCommand
{
    public required Guid Id { get; init; }
    public required Guid CourseId { get; init; }
}



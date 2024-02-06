namespace LearnMS.API.Features.Courses.Contracts;

public sealed record UpdateLessonCommand
{
    public required Guid Id { get; init; }
    public required Guid CourseId { get; init; }
    public required Guid LectureId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? VideoSrc { get; init; }
}

public sealed record UpdateLessonRequest
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? VideoSrc { get; init; }
}
namespace LearnMS.API.Features.Courses.Contracts;

public sealed record RenewLessonExpirationCommand
{
    public required Guid CourseId { get; init; }
    public required Guid LectureId { get; init; }
    public required Guid LessonId { get; init; }
    public required Guid StudentId { get; set; }
}
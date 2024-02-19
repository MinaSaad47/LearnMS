public sealed record ValidateLessonVideoStatusCommand
{
    public required Guid CourseId { get; set; }
    public required Guid LectureId { get; set; }
    public required Guid LessonId { get; set; }
}

public record ValidateLessonVideoStatusResult
{
    public required string VideoId;
    public required string Status;
}

public record ValidateLessonVideoStatusResponse : ValidateLessonVideoStatusResult;
namespace LearnMS.API.Features.Courses.Contracts;


public record GetLessonQuery
{
    public required Guid CourseId { get; init; }
    public required Guid LectureId { get; init; }
    public required Guid LessonId { get; init; }
}

public record GetLessonResult
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required decimal RenewalPrice { get; init; }
    public required int ExpirationHours { get; init; }
    public required string VideoSrc { get; init; }
}

public record GetLessonResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required int ExpirationHours { get; init; }
    public required decimal RenewalPrice { get; init; }
    public required string Description { get; init; }
    public required string VideoSrc { get; init; }
}


// for student

public record GetStudentLessonQuery
{
    public required Guid CourseId { get; init; }
    public required Guid LectureId { get; init; }
    public required Guid StudentId { get; init; }
    public required Guid LessonId { get; init; }
}

public record GetStudentLessonResult
{

    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required int ExpirationHours { get; init; }
    public required decimal RenewalPrice { get; init; }
    public required string Description { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public required string Enrollment { get; init; }
    public string? VideoSrc { get; init; }
}
public record GetStudentLessonResponse
{

    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required int ExpirationHours { get; init; }
    public required decimal RenewalPrice { get; init; }
    public required string Description { get; init; }
    public required string Enrollment { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public string? VideoSrc { get; init; }
}
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
    public required string VideoEmbed { get; init; }
}

public record GetLessonResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string VideoEmbed { get; init; }
}


// for student

public record GetStudentLessonQuery
{
    public required Guid CourseId { get; init; }
    public required Guid LectureId { get; init; }
    public required Guid StudentId { get; init; }
    public required Guid LessonId { get; init; }
}

public record GetStudentLessonResult : GetLessonResult { }
public record GetStudentLessonResponse : GetLessonResponse { }
namespace LearnMS.API.Features.Courses.Contracts;

public sealed record CreateLessonRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string VideoEmbed { get; set; }
}


public sealed record CreateLessonCommand
{
    public required Guid CourseId { get; set; }
    public required Guid LectureId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string VideoEmbed { get; set; }
}



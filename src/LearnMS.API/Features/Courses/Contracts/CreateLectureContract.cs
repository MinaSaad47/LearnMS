namespace LearnMS.API.Features.Courses.Contracts;

public sealed record CreateLectureRequest
{

    public required string Title { get; init; }
}

public sealed record CreateLectureCommand
{
    public required Guid CourseId { get; init; }
    public required string Title { get; init; }
}



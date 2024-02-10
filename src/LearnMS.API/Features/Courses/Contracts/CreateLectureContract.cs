using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Courses.Contracts;

public sealed record CreateLectureRequest
{

    [Required, Length(3, 100)]
    public required string Title { get; init; }
}

public sealed record CreateLectureCommand
{
    public required Guid CourseId { get; init; }
    public required string Title { get; init; }
}



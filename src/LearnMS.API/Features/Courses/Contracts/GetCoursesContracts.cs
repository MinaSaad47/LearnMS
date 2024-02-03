using LearnMS.API.Entities;

namespace LearnMS.API.Features.Courses.Contracts;

// for teacher or anyone

public sealed record GetCoursesQuery
{
    public CourseStatus? Status { get; set; }
}

public sealed record GetCoursesResult
{
    public required IEnumerable<SingleCourse> Items { get; init; }
}

public sealed record GetCoursesResponse
{
    public required IEnumerable<SingleCourse> Items { get; init; }
}

public record SingleCourse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }
    public required decimal? Price { get; init; }
    public required decimal? RenewalPrice { get; init; }
    public required CourseStatus Status { get; init; }
}

// for student

public sealed record GetStudentCoursesQuery
{
    public required Guid StudentId { get; init; }
}

public sealed record GetStudentCoursesResult
{
    public required IEnumerable<SingleStudentCourse> Items { get; init; }
}

public sealed record GetStudentCoursesResponse
{
    public required IEnumerable<SingleStudentCourse> Items { get; init; }
}

public sealed record SingleStudentCourse : SingleCourse
{
    public DateTime? ExpiresAt { get; init; }
    public bool? IsExpired { get; init; }
}
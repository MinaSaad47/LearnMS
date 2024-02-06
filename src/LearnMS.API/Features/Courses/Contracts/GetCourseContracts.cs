using LearnMS.API.Entities;

namespace LearnMS.API.Features.Courses.Contracts;

// for teacher or anyone who wants to get a course

public record GetCourseQuery
{
    public required Guid Id { get; init; }
    public bool? IsCourseItemPublished { get; init; }
}

public record GetCourseResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }
    public required decimal? Price { get; init; }
    public required decimal? RenewalPrice { get; init; }
    public required bool IsPublished { get; init; }
    public required int? ExpirationDays { get; init; }
    public required IEnumerable<SingleCourseItem> Items { get; init; } = [];
}

public record GetCourseResult
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }
    public required decimal? Price { get; init; }
    public required decimal? RenewalPrice { get; init; }
    public required int? ExpirationDays { get; init; }
    public required bool IsPublished { get; init; }
    public IEnumerable<SingleCourseItem> Items { get; set; } = [];
}

public enum CourseItemType
{
    Lecture,
    Exam
};

public record SingleCourseItem
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required int Order { get; init; }
    public decimal? Price { get; init; }
    public decimal? RenewalPrice { get; init; }
    public required string Type { get; init; }
    public string? ImageUrl { get; init; }
}


// for student purchased the course

public sealed record GetStudentCourseQuery : GetCourseQuery
{
    public required Guid StudentId { get; init; }
}

public sealed record GetStudentCourseResult
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }
    public required decimal? Price { get; init; }
    public required string Enrollment { get; set; }
    public required decimal? RenewalPrice { get; init; }
    public required DateTime? ExpiresAt { get; init; }
    public required int? ExpirationDays { get; init; }
    public IEnumerable<SingleStudentCourseItem> Items { get; set; } = [];
}

public sealed record GetStudentCourseResponse
{
    public required DateTime? ExpiresAt { get; init; }
    public required IEnumerable<SingleStudentCourseItem> Items { get; init; }
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }
    public required string Enrollment { get; set; }
    public required decimal? Price { get; init; }
    public required decimal? RenewalPrice { get; init; }
    public required int? ExpirationDays { get; init; }
}

public sealed record SingleStudentCourseItem : SingleCourseItem
{
    public required string Enrollment { get; set; }
    public required DateTime? ExpiresAt { get; init; }
}
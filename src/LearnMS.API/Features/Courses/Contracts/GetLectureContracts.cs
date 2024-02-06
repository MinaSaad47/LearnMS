using LearnMS.API.Entities;

namespace LearnMS.API.Features.Courses.Contracts;


// for teacher and anyone who want to purchase a lecture

public sealed record GetLectureQuery
{
    public required Guid CourseId { get; set; }
    public required Guid LectureId { get; set; }
    public bool? IsPublished { get; set; }
    public bool? IsCoursePublished { get; set; }
}

public record GetLectureResult
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public decimal? RenewalPrice { get; set; }
    public required bool? IsPublished { get; set; }
    public required int? ExpirationDays { get; set; }
    public List<SingleLectureItem> Items { get; set; } = new();
}

public record GetLectureResponse
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public decimal? RenewalPrice { get; set; }
    public required int? ExpirationDays { get; set; }
    public required bool? IsPublished { get; set; }
    public List<SingleLectureItem> Items { get; set; } = new();
}

public record SingleLectureItem
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Type { get; set; }
    public required string? Description { get; set; }
}


// for student

public sealed record GetStudentLectureQuery
{
    public required Guid CourseId { get; set; }
    public required Guid LectureId { get; set; }
    public required Guid StudentId { get; set; }
}

public sealed record GetStudentLectureResult
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public decimal? RenewalPrice { get; set; }
    public List<SingleLectureItem> Items { get; set; } = new();
    public required DateTime? ExpiresAt { get; set; }
    public required string? Enrollment { get; set; }
}

public sealed record GetStudentLectureResponse
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public decimal? RenewalPrice { get; set; }
    public List<SingleLectureItem> Items { get; set; } = new();
    public required DateTime? ExpiresAt { get; set; }
    public required string? Enrollment { get; set; }
}
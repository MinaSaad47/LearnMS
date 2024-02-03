using LearnMS.API.Entities;

namespace LearnMS.API.Features.Courses.Contracts;


// for teacher and anyone who want to purchase a lecture

public sealed record GetLectureQuery
{
    public required Guid CourseId { get; set; }
    public required Guid LectureId { get; set; }
    public CourseItemStatus? LectureStatus { get; set; }
    public CourseStatus? CourseStatus { get; set; }
}

public record GetLectureResult
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public decimal? RenewalPrice { get; set; }
    public required CourseItemStatus Status { get; set; }
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
    public required CourseItemStatus Status { get; set; }
    public List<SingleLectureItem> Items { get; set; } = new();
}

public record SingleLectureItem
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string? Description { get; set; }
    public required string? ImageUrl { get; set; }
}


// for student

public sealed record GetStudentLectureQuery
{
    public required Guid CourseId { get; set; }
    public required Guid LectureId { get; set; }
    public required Guid StudentId { get; set; }
}

public sealed record GetStudentLectureResult : GetLectureResult
{
    public required DateTime? ExpiresAt { get; set; }
}

public sealed record GetStudentLectureResponse : GetLectureResponse
{
    public required DateTime? ExpiresAt { get; set; }
}
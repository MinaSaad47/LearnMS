using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Courses.Contracts;

public sealed record UpdateLessonCommand
{
    public required Guid Id { get; init; }
    public required Guid CourseId { get; init; }
    public required Guid LectureId { get; init; }
    public string? Title { get; init; }
    public decimal? RenewalPrice { get; init; }
    public string? Description { get; init; }
    public int? ExpirationHours { get; init; }
}

public sealed record UpdateLessonRequest
{
    [Length(0, 100)]
    public string? Title { get; init; }
    [Length(0, 1000)]
    public string? Description { get; init; }
    [Range(0, 100)]
    public decimal? RenewalPrice { get; init; }
    [Range(0, 24)]
    public int? ExpirationHours { get; init; }
}
using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Courses.Contracts;

public sealed record UpdateLectureCommand
{
    public required Guid Id { get; init; }
    public required Guid CourseId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
    public decimal? Price { get; init; }
    public decimal? RenewalPrice { get; init; }
    public int? ExpirationDays { get; init; }
}


public sealed record UpdateLectureRequest
{
    public Guid CourseId { get; init; }
    [Length(0, 100)]
    public string? Title { get; init; }
    [Length(0, 1000)]
    public string? Description { get; init; }
    [Range(0, 100)]
    public decimal? Price { get; init; }
    [Url]
    public string? ImageUrl { get; init; }
    [Range(0, 100)]
    public decimal? RenewalPrice { get; init; }
    [Range(0, 1000)]
    public int? ExpirationDays { get; init; }
}

using System.ComponentModel.DataAnnotations;
using LearnMS.API.Entities;

namespace LearnMS.API.Features.Courses.Contracts;

public record UpdateCourseRequest
{
    [Length(0, 100)]
    public string? Title { get; init; }
    [Length(0, 1000)]
    public string? Description { get; init; }
    [Range(0, 1000)]
    public decimal? Price { get; init; }
    [Range(0, 1000)]
    public decimal? RenewalPrice { get; init; }
    public StudentLevel? Level { get; init; }
    [Range(0, 1000)]
    public int? ExpirationDays { get; init; }
    [Url]
    public string? ImageUrl { get; init; }
}

public record UpdateCourseCommand
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public StudentLevel? Level { get; init; }
    public decimal? RenewalPrice { get; init; }
    public string? ImageUrl { get; init; }
    public int? ExpirationDays { get; init; }
}
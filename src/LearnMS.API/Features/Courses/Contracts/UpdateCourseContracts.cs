using LearnMS.API.Entities;

namespace LearnMS.API.Features.Courses.Contracts;

public record UpdateCourseRequest
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public decimal? RenewalPrice { get; init; }
    public int? ExpirationDays { get; init; }
}

public record UpdateCourseCommand
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public decimal? RenewalPrice { get; init; }
    public int? ExpirationDays { get; init; }
}
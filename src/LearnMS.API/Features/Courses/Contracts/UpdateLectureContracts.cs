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
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public string? ImageUrl { get; init; }
    public decimal? RenewalPrice { get; init; }
    public int? ExpirationDays { get; init; }
}

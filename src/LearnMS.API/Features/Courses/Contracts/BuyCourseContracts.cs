namespace LearnMS.API.Features.Courses.Contracts;

public record BuyCourseCommand
{
    public Guid CourseId { get; init; }
    public Guid StudentId { get; init; }
}

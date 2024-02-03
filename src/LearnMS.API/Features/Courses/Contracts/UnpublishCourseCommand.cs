namespace LearnMS.API.Features.Courses.Contracts;

public sealed record UnPublishCourseCommand
{
    public Guid Id { get; set; }
}
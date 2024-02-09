namespace LearnMS.API.Features.Courses.Contracts;

public sealed record DeleteLectureCommand
{
    public required Guid CourseId { get; set; }
    public required Guid Id { get; set; }
}
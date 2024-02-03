namespace LearnMS.API.Features.Courses.Contracts;

public record BuyLectureCommand
{
    public Guid CourseId { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentId { get; set; }
}
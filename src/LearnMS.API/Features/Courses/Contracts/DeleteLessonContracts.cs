namespace LearnMS.API.Features.Courses.Contracts;

public class DeleteLessonCommand
{
    public required Guid CourseId { get; set; }
    public required Guid LectureId { get; set; }
    public required Guid Id { get; set; }
}
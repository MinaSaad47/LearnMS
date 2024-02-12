namespace LearnMS.API.Entities;

public sealed class StudentLesson
{
    public required Guid StudentId { get; init; }
    public required Guid LessonId { get; init; }
    public required DateTime ExpirationDate { get; set; }
}
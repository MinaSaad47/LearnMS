namespace LearnMS.API.Entities;

public sealed class StudentCourse
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime ExpirationDate { get; set; }
}
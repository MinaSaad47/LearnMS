namespace LearnMS.API.Entities;

public sealed class StudentExam
{
    public Guid StudentId { get; set; }
    public Guid ExamId { get; set; }
    public DateTime ExpirationDate { get; set; }
}
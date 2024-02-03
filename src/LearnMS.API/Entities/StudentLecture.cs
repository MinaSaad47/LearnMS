namespace LearnMS.API.Entities;

public sealed class StudentLecture
{
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public DateTime ExpirationDate { get; set; }
}
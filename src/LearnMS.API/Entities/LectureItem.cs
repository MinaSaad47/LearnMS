namespace LearnMS.API.Entities;

public class LectureItem
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public Guid LectureId { get; set; }
}
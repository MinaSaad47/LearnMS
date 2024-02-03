namespace LearnMS.API.Entities;

public class Lesson
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string VideoEmbed { get; set; }
}
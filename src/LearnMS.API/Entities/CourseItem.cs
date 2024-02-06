namespace LearnMS.API.Entities;


public sealed class CourseItem
{
    public Guid Id { get; set; }

    public int Order { get; set; }

    public Guid CourseId { get; set; }
    public bool IsPublished { get; set; } = false;
}
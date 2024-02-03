namespace LearnMS.API.Entities;

public enum CourseItemStatus
{
    Published,
    Draft,
    Hidden,
}

public sealed class CourseItem
{
    public Guid Id { get; set; }

    public int Order { get; set; }

    public Guid CourseId { get; set; }
    public CourseItemStatus Status { get; set; } = CourseItemStatus.Draft;
}
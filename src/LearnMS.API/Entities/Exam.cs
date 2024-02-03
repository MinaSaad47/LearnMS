namespace LearnMS.API.Entities;

public sealed class Exam
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required decimal? Price { get; set; }
    public required decimal? RenewalPrice { get; set; }
}
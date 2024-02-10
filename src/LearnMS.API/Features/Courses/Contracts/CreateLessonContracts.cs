using System.ComponentModel.DataAnnotations;

namespace LearnMS.API.Features.Courses.Contracts;

public sealed record CreateLessonRequest
{
    [Required, Length(3, 100)]
    public required string Title { get; set; }
    [Required, Length(10, 1000)]
    public required string Description { get; set; }
    [Required, Range(1, 100)]
    public required decimal RenewalPrice { get; set; }
    [Required, Range(1, 24)]
    public required int ExpirationHours { get; set; }
    [Required, Url]
    public required string VideoSrc { get; set; }
}


public sealed record CreateLessonCommand
{
    public required Guid CourseId { get; set; }
    public required Guid LectureId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal RenewalPrice { get; set; }
    public required int ExpirationHours { get; set; }
    public required string VideoSrc { get; set; }
}



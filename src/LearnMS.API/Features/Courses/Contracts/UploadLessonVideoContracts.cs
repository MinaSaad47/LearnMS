using LearnMS.API.ThirdParties;

namespace LearnMS.API.Features.Courses.Contracts;

public record UploadLessonVideoCommand
{
    public required Guid CourseId { get; set; }
    public required Guid LectureId { get; set; }
    public required Guid LessonId { get; set; }
    public required Stream FS { get; set; }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnMS.API.Entities;

public class Lesson
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal RenewalPrice { get; set; }
    public required int ExpirationHours { get; set; }
    public string? VideoId { get; set; }
    public LessonVideoStatus VideoStatus { get; set; } = LessonVideoStatus.NoVideo;
}

[JsonConverter(typeof(StringEnumConverter))]
public enum LessonVideoStatus
{
    NoVideo,
    Processing,
    Ready,
}
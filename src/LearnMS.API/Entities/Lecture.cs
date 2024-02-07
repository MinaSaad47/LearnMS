namespace LearnMS.API.Entities;


public class Lecture
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    public decimal? Price { get; set; }
    public decimal? RenewalPrice { get; set; }
    public int? ExpirationDays { get; set; }

    public List<LectureItem> Items = new();

    public void AddLesson(Lesson lesson, out Lesson addedLesson)
    {
        var item = new LectureItem
        {
            Id = Guid.NewGuid(),
            Order = Items.Count,
            LectureId = Id
        };
        Items.Add(item);
        addedLesson = lesson;
        addedLesson.Id = item.Id;
    }

    public bool IsPublishable
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Title)) return false;
            if (string.IsNullOrWhiteSpace(Description)) return false;
            if (string.IsNullOrWhiteSpace(ImageUrl)) return false;
            if (Price is null) return false;
            if (RenewalPrice is null) return false;
            if (ExpirationDays == null) return false;
            return true;
        }
    }
}
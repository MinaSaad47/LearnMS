namespace LearnMS.API.Entities;

public sealed class Course
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public decimal? RenewalPrice { get; set; }

    public List<CourseItem> Items = new();
    public int? ExpirationDays { get; set; }

    public bool IsPublished { get; set; } = false;

    public void AddLecture(Lecture lecture, out Lecture addedLecture)
    {
        var item = new CourseItem
        {
            CourseId = Id,
            Id = Guid.NewGuid(),
            Order = Items.Count,
        };
        Items.Add(item);
        addedLecture = lecture;
        addedLecture.Id = item.Id;
    }

    public bool IsPublishable
    {
        get
        {

            if (string.IsNullOrWhiteSpace(Title)) return false;
            if (string.IsNullOrWhiteSpace(Description)) return false;
            // if (string.IsNullOrWhiteSpace(ImageUrl)) return false;
            if (Price == null) return false;
            if (RenewalPrice == null) return false;
            if (ExpirationDays == null) return false;
            return true;
        }
    }

}

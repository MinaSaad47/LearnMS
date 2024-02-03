using LearnMS.API.Common;

namespace LearnMS.API.Features.Courses;

public static class LecturesErrors
{
    public static readonly ApiError NotFound = new ApiError("lecture/not-found", "Course not found", StatusCodes.Status404NotFound);
    public static readonly ApiError NotPublishable = new ApiError("lecture/not-publishable", "Lecture is not publishable, please complete lecture creation", StatusCodes.Status403Forbidden);
    public static readonly ApiError AlreadyPurchased = new ApiError("lecture/already-published", "Lecture already published", StatusCodes.Status403Forbidden);
}
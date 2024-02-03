using LearnMS.API.Common;

namespace LearnMS.API.Features.Courses;

public static class CoursesErrors
{
    public static readonly ApiError NotFound = new ApiError("course/not-found", "Course not found", StatusCodes.Status404NotFound);
    public static readonly ApiError NotPublishable = new ApiError("course/not-publishable", "Course not publishable, please complete the course creation", StatusCodes.Status403Forbidden);
    public static readonly ApiError AlreadyPurchased = new ApiError("course/already-published", "Course already published", StatusCodes.Status403Forbidden);
}
using LearnMS.API.Common;

namespace LearnMS.API.Features.Courses;

public static class LessonsErrors
{
    public static readonly ApiError NotFound = new ApiError("lesson/not-found", "Lesson not found", StatusCodes.Status404NotFound);
}
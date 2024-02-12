using LearnMS.API.Common;

namespace LearnMS.API.Features.Courses;

public static class LessonsErrors
{
    public static readonly ApiError NotFound = new ApiError("lesson/not-found", "Lesson not found", StatusCodes.Status404NotFound);
    public static readonly ApiError Expired = new ApiError("lesson/expired", "Lesson expired", StatusCodes.Status403Forbidden);
    public static readonly ApiError NotAcceptedExpirationRule = new ApiError("lesson/not-accepted-expiration-rule", "Lesson expiration rule not accepted", StatusCodes.Status403Forbidden);
    public static readonly ApiError AlreadyAcceptedExpirationRule = new ApiError("lesson/already-accepted-expiration-rule", "Lesson expiration rule already accepted", StatusCodes.Status403Forbidden);
    public static readonly ApiError AlreadyRenewed = new ApiError("lesson/already-renewed", "Lesson already renewed", StatusCodes.Status403Forbidden);
}
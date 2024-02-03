using LearnMS.API.Common;

namespace LearnMS.API.Features.Students;
public static class StudentsErrors
{
    public static readonly ApiError NotFound = new("student/not-found", "Student not found.", StatusCodes.Status404NotFound);
}
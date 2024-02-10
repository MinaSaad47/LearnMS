using LearnMS.API.Common;
using LearnMS.API.Features.Courses.Contracts;
using LearnMS.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.Courses.Lectures.Lessons;

[Route("api/courses/{courseId:guid}/lectures/{lectureId:guid}/lessons")]
[Tags("Lessons")]
public sealed class LessonsController : ControllerBase
{
    private readonly ICoursesService _coursesService;
    private readonly ICurrentUserService _currentUserService;

    public LessonsController(ICoursesService coursesService, ICurrentUserService currentUserService)
    {
        _coursesService = coursesService;
        _currentUserService = currentUserService;
    }

    [HttpDelete("{lessonId:guid}")]
    public async Task<ApiWrapper.Success<object?>> Delete(Guid courseId, Guid lectureId, Guid lessonId)
    {
        await _coursesService.ExecuteAsync(new DeleteLessonCommand
        {
            CourseId = courseId,
            Id = lessonId,
            LectureId = lectureId
        });
        return new()
        {
            Message = "Lesson deleted successfully"
        };
    }

    [HttpGet("{lessonId:guid}")]
    [ProducesResponseType(typeof(GetLessonResponse), 200)]
    [ProducesResponseType(typeof(GetStudentLessonResponse), 200)]
    [ApiAuthorize()]
    public async Task<ApiWrapper.Success<object>> Get(Guid courseId, Guid lectureId, Guid lessonId)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        object response;

        if (currentUser!.Role != UserRole.Student)
        {
            var result = await _coursesService.QueryAsync(new GetLessonQuery { CourseId = courseId, LessonId = lessonId, LectureId = lectureId });

            response = new GetLessonResponse
            {
                ExpirationHours = result.ExpirationHours,
                Description = result.Description,
                RenewalPrice = result.RenewalPrice,
                Id = result.Id,
                Title = result.Title,
                VideoSrc = result.VideoSrc
            };
        }
        else
        {

            var result = await _coursesService.QueryAsync(new GetStudentLessonQuery { CourseId = courseId, LessonId = lessonId, LectureId = lectureId, StudentId = currentUser.Id });

            response = new GetStudentLessonResponse
            {
                ExpirationHours = result.ExpirationHours,
                Enrollment = result.Enrollment,
                ExpiresAt = result.ExpiresAt,
                RenewalPrice = result.RenewalPrice,
                Description = result.Description,
                Id = result.Id,
                Title = result.Title,
                VideoSrc = result.VideoSrc
            };
        }

        return new()
        {
            Data = response,
            Message = "Lesson retrieved successfully"
        };
    }

    [HttpPost]
    public async Task<ApiWrapper.Success<object?>> Post([FromForm] CreateLessonRequest request, Guid lectureId, Guid courseId)
    {
        await _coursesService.ExecuteAsync(new CreateLessonCommand
        {
            VideoSrc = request.VideoSrc,
            RenewalPrice = request.RenewalPrice,
            Title = request.Title,
            CourseId = courseId,
            ExpirationHours = request.ExpirationHours,
            Description = request.Description,
            LectureId = lectureId
        });

        Response.StatusCode = StatusCodes.Status201Created;

        return new()
        {
            Message = "Created lesson successfully"
        };
    }

    [HttpPatch("{lessonId:guid}")]
    public async Task<ApiWrapper.Success<object?>> Patch([FromBody] UpdateLessonRequest request, Guid lessonId, Guid courseId, Guid lectureId)
    {
        await _coursesService.ExecuteAsync(new UpdateLessonCommand
        {
            Id = lessonId,
            Title = request.Title,
            Description = request.Description,
            ExpirationHours = request.ExpirationHours,
            VideoSrc = request.VideoSrc,
            RenewalPrice = request.RenewalPrice,
            CourseId = courseId,
            LectureId = lectureId
        });

        return new()
        {
            Message = "Updated lesson successfully"
        };
    }

    [HttpPost("{lessonId:guid}/start")]
    [ApiAuthorize(Role = UserRole.Student)]
    public async Task<ApiWrapper.Success<object?>> Start(Guid lessonId, Guid courseId, Guid lectureId)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        await _coursesService.ExecuteAsync(new StartLessonCommand
        {
            CourseId = courseId,
            LessonId = lessonId,
            LectureId = lectureId,
            StudentId = currentUser!.Id
        });

        return new()
        {
            Message = "Accepted expiration rule successfully"
        };
    }

    [HttpPost("{lessonId:guid}/renew")]
    [ApiAuthorize(Role = UserRole.Student)]
    public async Task<ApiWrapper.Success<object?>> RenewExpiration(Guid lessonId, Guid courseId, Guid lectureId)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        await _coursesService.ExecuteAsync(new RenewLessonExpirationCommand
        {
            CourseId = courseId,
            LessonId = lessonId,
            LectureId = lectureId,
            StudentId = currentUser!.Id
        });

        return new()
        {
            Message = "Renewed expiration successfully"
        };

    }
}
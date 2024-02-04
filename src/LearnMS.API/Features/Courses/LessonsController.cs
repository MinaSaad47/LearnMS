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

    [HttpGet("{lessonId:guid}")]
    [ApiAuthorize()]
    public async Task<ApiWrapper.Success<GetLessonResponse>> Get(Guid courseId, Guid lectureId, Guid lessonId)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        GetLessonResponse response;

        if (currentUser!.Role != UserRole.Student)
        {
            var result = await _coursesService.QueryAsync(new GetLessonQuery { CourseId = courseId, LessonId = lessonId, LectureId = lectureId });

            response = new GetLessonResponse
            {
                Description = result.Description,
                Id = result.Id,
                Title = result.Title,
                VideoEmbed = result.VideoEmbed
            };
        }
        else
        {

            var result = await _coursesService.QueryAsync(new GetStudentLessonQuery { CourseId = courseId, LessonId = lessonId, LectureId = lectureId, StudentId = currentUser.Id });

            response = new GetStudentLessonResponse
            {
                Description = result.Description,
                Id = result.Id,
                Title = result.Title,
                VideoEmbed = result.VideoEmbed
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
            VideoEmbed = request.VideoEmbed,
            Title = request.Title,
            CourseId = courseId,
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
            CourseId = courseId,
            LectureId = lectureId
        });

        return new()
        {
            Message = "Updated lesson successfully"
        };
    }


}
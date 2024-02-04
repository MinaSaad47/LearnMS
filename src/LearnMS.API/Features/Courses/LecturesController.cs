using LearnMS.API.Common;
using LearnMS.API.Features.Courses.Contracts;
using LearnMS.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.Courses.Lectures;

[Route("/api/courses/{courseId}/lectures")]
[Tags("Lectures")]
public sealed class LecturesController : ControllerBase
{
    private readonly ICoursesService _coursesService;
    private readonly ICurrentUserService _currentUserService;
    public LecturesController(ICoursesService coursesService, ICurrentUserService currentUserService)
    {
        _coursesService = coursesService;
        _currentUserService = currentUserService;
    }

    [HttpPost("{lectureId:guid}/buy")]
    [ApiAuthorize(Role = UserRole.Student)]
    public async Task<ApiWrapper.Success<object?>> Buy(Guid lectureId, Guid courseId)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        await _coursesService.ExecuteAsync(new BuyLectureCommand
        {
            CourseId = courseId,
            LectureId = lectureId,
            StudentId = currentUser!.Id
        });

        return new()
        {
            Message = "Lecture purchased successfully"
        };
    }

    [HttpGet("{lectureId:guid}")]
    public async Task<ApiWrapper.Success<GetLectureResponse>> Get(Guid lectureId, Guid courseId)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        GetLectureResponse response;

        if (currentUser is null)
        {
            var result = await _coursesService.QueryAsync(new GetLectureQuery
            {
                LectureId = lectureId,
                CourseId = courseId,
                CourseStatus = Entities.CourseStatus.Published,
                LectureStatus = Entities.CourseItemStatus.Published
            });

            response = new GetLectureResponse
            {
                Description = result.Description,
                Id = result.Id,
                Title = result.Title,
                Status = result.Status,
                ImageUrl = result.ImageUrl,
                Items = result.Items,
                Price = result.Price,
                RenewalPrice = result.RenewalPrice,
            };
        }
        else if (currentUser.Role == UserRole.Student)
        {
            var result = await _coursesService.QueryAsync(new GetStudentLectureQuery { CourseId = courseId, LectureId = lectureId, StudentId = currentUser.Id });

            response = new GetStudentLectureResponse
            {
                ExpiresAt = result.ExpiresAt,
                Description = result.Description,
                Id = result.Id,
                Title = result.Title,
                IsExpired = result.IsExpired,
                Status = result.Status,
                ImageUrl = result.ImageUrl,
                Items = result.Items,
                Price = result.Price,
                RenewalPrice = result.RenewalPrice,
            };
        }
        else
        {

            var result = await _coursesService.QueryAsync(new GetLectureQuery
            {
                LectureId = lectureId,
                CourseId = courseId,
            });

            response = new GetLectureResponse
            {
                Description = result.Description,
                Id = result.Id,
                Title = result.Title,
                Status = result.Status,
                ImageUrl = result.ImageUrl,
                Items = result.Items,
                Price = result.Price,
                RenewalPrice = result.RenewalPrice,
            };
        }


        return new()
        {
            Data = response,
            Message = "Fetched lecture successfully"
        };
    }


    [HttpPost]
    public async Task<ApiWrapper.Success<object?>> Post([FromBody] CreateLectureRequest request, Guid courseId)
    {
        await _coursesService.ExecuteAsync(new CreateLectureCommand
        {
            CourseId = courseId,
            Title = request.Title
        });

        return new()
        {
            Message = "Created lecture successfully"
        };
    }

    [HttpPatch("{lectureId:guid}")]
    public async Task<ApiWrapper.Success<object?>> Patch([FromBody] UpdateLectureRequest request, Guid lectureId, Guid courseId)
    {
        await _coursesService.ExecuteAsync(new UpdateLectureCommand
        {
            CourseId = courseId,
            ImageUrl = request.ImageUrl,
            Id = lectureId,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            ExpirationDays = request.ExpirationDays,
            RenewalPrice = request.RenewalPrice,
        });

        return new()
        {
            Message = "Updated course successfully"
        };
    }

    [HttpPost("{lectureId:guid}/publish")]
    public async Task<ApiWrapper.Success<object?>> Publish(Guid lectureId, Guid courseId)
    {
        await _coursesService.ExecuteAsync(new PublishLectureCommand
        {
            Id = lectureId,
            CourseId = courseId
        }); ;

        return new()
        {
            Message = "Published lecture successfully"
        };
    }

    [HttpPost("{lectureId:guid}/unpublish")]
    public async Task<ApiWrapper.Success<object?>> Unpublish(Guid lectureId, Guid courseId)
    {
        await _coursesService.ExecuteAsync(new UnPublishLectureCommand
        {
            Id = lectureId,
            CourseId = courseId
        });

        return new()
        {
            Message = "Unpublished lecture successfully"
        };
    }
}
using LearnMS.API.Common;
using LearnMS.API.Entities;
using LearnMS.API.Features.Courses.Contracts;
using LearnMS.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.Courses;

[Route("api/courses")]
[Tags("Courses")]
public sealed class CoursesController : ControllerBase
{
    ICoursesService _coursesService;
    ICurrentUserService _currentUserService;

    public CoursesController(ICoursesService coursesService, ICurrentUserService currentUserService)
    {
        _coursesService = coursesService;
        _currentUserService = currentUserService;
    }

    [HttpPost("{courseId:guid}/buy")]
    [ApiAuthorize(Role = UserRole.Student)]
    public async Task<ApiWrapper.Success<object?>> Buy(Guid courseId)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        await _coursesService.ExecuteAsync(new BuyCourseCommand
        {
            CourseId = courseId,
            StudentId = currentUser!.Id
        });

        return new()
        {
            Message = "Course purchased successfully"
        };
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiWrapper.Success<GetCoursesResponse>), 200)]
    [ProducesResponseType(typeof(ApiWrapper.Success<GetStudentCoursesResponse>), 200)]
    public async Task<ApiWrapper.Success<object>> Get()
    {
        var currentUser = await _currentUserService.GetUserAsync();

        object response;

        int count = 0;

        if (currentUser is null)
        {
            var result = await _coursesService.QueryAsync(new GetCoursesQuery { IsPublished = true });
            response = new GetCoursesResponse
            {
                Items = result.Items
            };
            count = result.Items.Count();
        }
        else if (currentUser.Role == UserRole.Student)
        {

            var result = await _coursesService.QueryAsync(new GetStudentCoursesQuery { StudentId = currentUser.Id });

            response = new GetStudentCoursesResponse
            {
                Items = result.Items
            };
            count = result.Items.Count();
        }
        else
        {
            var result = await _coursesService.QueryAsync(new GetCoursesQuery { });

            response = new GetCoursesResponse
            {
                Items = result.Items
            };
            count = result.Items.Count();
        }


        return new()
        {
            Data = response,
            Message = count == 0 ? "No courses found" : "Courses retrieved successfully"
        };
    }

    [HttpGet("{courseId:guid}")]
    [ProducesResponseType(typeof(ApiWrapper.Success<GetCourseResponse>), 200)]
    [ProducesResponseType(typeof(ApiWrapper.Success<GetStudentCourseResponse>), 200)]
    public async Task<ApiWrapper.Success<object>> Get(Guid courseId)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        object response;

        if (currentUser is null)
        {
            var result = await _coursesService.QueryAsync(new GetCourseQuery { Id = courseId, IsCourseItemPublished = true });
            response = new GetCourseResponse
            {
                Id = result.Id,
                Level = result.Level,
                ExpirationDays = result.ExpirationDays,
                Description = result.Description,
                ImageUrl = result.ImageUrl,
                Price = result.Price,
                RenewalPrice = result.RenewalPrice,
                Title = result.Title,
                IsPublished = result.IsPublished,
                Items = result.Items
            };
        }
        else if (currentUser.Role == UserRole.Student)
        {

            var result = await _coursesService.QueryAsync(new GetStudentCourseQuery { Id = courseId, IsCourseItemPublished = true, StudentId = currentUser.Id });

            response = new GetStudentCourseResponse
            {
                Id = result.Id,
                Level = result.Level,
                ExpirationDays = result.ExpirationDays,
                Description = result.Description,
                ImageUrl = result.ImageUrl,
                Price = result.Price,
                RenewalPrice = result.RenewalPrice,
                Title = result.Title,
                Items = result.Items,
                ExpiresAt = result.ExpiresAt,
                Enrollment = result.Enrollment
            };
        }
        else
        {

            var result = await _coursesService.QueryAsync(new GetCourseQuery { Id = courseId });
            response = new GetCourseResponse
            {
                Id = result.Id,
                Description = result.Description,
                ImageUrl = result.ImageUrl,
                Level = result.Level,
                Price = result.Price,
                RenewalPrice = result.RenewalPrice,
                Title = result.Title,
                IsPublished = result.IsPublished,
                ExpirationDays = result.ExpirationDays,
                Items = result.Items
            };

        }


        return new()
        {
            Data = response,
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ApiAuthorize(Permissions = [Permission.ManageCourses])]
    public async Task<ApiWrapper.Success<CreateCourseResponse>> Post([FromBody] CreateCourseCommand request)
    {
        var result = await _coursesService.ExecuteAsync(request);

        Response.StatusCode = StatusCodes.Status201Created;

        return new()
        {
            Message = "Course created successfully"
                ,
            Data = new()
            {
                Id = result.Id
            }
        };
    }

    [HttpPatch("{courseId:guid}")]
    public async Task<ApiWrapper.Success<object?>> Patch([FromBody] UpdateCourseRequest request, Guid courseId)
    {
        await _coursesService.ExecuteAsync(new UpdateCourseCommand
        {
            ImageUrl = request.ImageUrl,
            Id = courseId,
            Title = request.Title,
            Description = request.Description,
            Level = request.Level,
            Price = request.Price,
            RenewalPrice = request.RenewalPrice,
            ExpirationDays = request.ExpirationDays
        });

        Response.StatusCode = StatusCodes.Status200OK;

        return new()
        {
            Message = "Course created successfully"
        };
    }

    [HttpPost("{courseId:guid}/publish")]
    public async Task<ApiWrapper.Success<object?>> Publish(Guid courseId)
    {
        await _coursesService.ExecuteAsync(new PublishCourseCommand
        {
            Id = courseId
        });
        return new()
        {
            Message = "Course created successfully"
        };
    }


    [HttpPost("{courseId:guid}/unpublish")]
    public async Task<ApiWrapper.Success<object?>> Unpublish(Guid courseId)
    {
        await _coursesService.ExecuteAsync(new UnPublishCourseCommand
        {
            Id = courseId
        });
        return new()
        {
            Message = "Course created successfully"
        };
    }

}
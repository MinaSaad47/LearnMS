using LearnMS.API.Common;
using LearnMS.API.Entities;
using LearnMS.API.Features.Students.Contracts;
using LearnMS.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.Students;

[Route("api/students")]
[Tags("Students")]
public sealed class StudentsController(IStudentsService studentsService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageStudents])]
    public async Task<ApiWrapper.Success<PageList<SingleStudent>>> Get(string search, int? page, int? pageSize)
    {
        var result = await studentsService.QueryAsync(new GetStudentsQuery
        {
            Page = page,
            PageSize = pageSize,
            Search = search
        });

        return new()
        {
            Data = result,
            Message = result.Items.Count() > 0 ? "Retrieved students successfully" : "No students found"
        };
    }

    [HttpPost]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageStudents])]
    public async Task<ApiWrapper.Success<object?>> Post(CreateStudentRequest request)
    {
        await studentsService.ExecuteAsync(new CreateStudentCommand
        {
            Email = request.Email,
            Password = request.Password,
            FullName = request.FullName,
            Level = request.Level,
            ParentPhoneNumber = request.ParentPhoneNumber,
            PhoneNumber = request.PhoneNumber,
            School = request.School
        });

        return new()
        {
            Message = "Create student successfully",
        };
    }
}
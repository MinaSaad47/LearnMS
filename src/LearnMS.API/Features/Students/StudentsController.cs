using LearnMS.API.Common;
using LearnMS.API.Entities;
using LearnMS.API.Features.Students.Contracts;
using LearnMS.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.Students;

[Route("api/students")]
[Tags("Students")]
public sealed class StudentsController(IStudentsService studentsService, ICurrentUserService currentUserService) : ControllerBase
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

    [HttpDelete("{studentId:guid}")]
    public async Task<ApiWrapper.Success<object?>> Delete(Guid studentId)
    {
        await studentsService.ExecuteAsync(new DeleteStudentCommand
        {
            Id = studentId
        });
        return new()
        {
            Message = "Deleted all students"
        };
    }

    [HttpPost]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageStudents])]
    public async Task<ApiWrapper.Success<object?>> Post([FromBody] CreateStudentRequest request)
    {
        await studentsService.ExecuteAsync(new CreateStudentCommand
        {
            Email = request.Email,
            Password = request.Password,
            FullName = request.FullName,
            Level = request.Level,
            ParentPhoneNumber = request.ParentPhoneNumber,
            StudentCode = request.StudentCode,
            PhoneNumber = request.PhoneNumber,
            School = request.School
        });

        return new()
        {
            Message = "Create student successfully",
        };
    }

    [HttpPost("{studentId:guid}/credit")]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageStudents])]
    public async Task<ApiWrapper.Success<object?>> AddCredit([FromBody] AddStudentCreditRequest request, Guid studentId)
    {
        var currentUser = await currentUserService.GetUserAsync();

        await studentsService.ExecuteAsync(new AddStudentCreditCommand
        {
            Amount = request.Amount,
            Id = studentId,
            AssistantId = currentUser!.Role == UserRole.Assistant ? currentUser.Id : null
        });

        return new()
        {
            Message = "Added credit successfully"
        };
    }

    [HttpGet("{studentId:guid}")]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageStudents])]
    public async Task<ApiWrapper.Success<GetStudentResponse>> Get(Guid studentId)
    {
        var result = await studentsService.QueryAsync(new GetStudentQuery
        {
            Id = studentId
        });
        return new()
        {
            Data = new()
            {
                Credit = result.Credit,
                Email = result.Email,
                FullName = result.FullName,
                Level = result.Level,
                ParentPhoneNumber = result.ParentPhoneNumber,
                StudentCode = result.StudentCode,
                PhoneNumber = result.PhoneNumber,
                ProfilePicture = result.ProfilePicture,
                SchoolName = result.SchoolName,
                Id = result.Id,
                IsVerified = result.IsVerified
            }
        };
    }

    [HttpPatch("{studentId:guid}")]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageStudents])]
    public async Task<ApiWrapper.Success<object?>> Patch([FromBody] UpdateStudentRequest request, Guid studentId)
    {
        await studentsService.ExecuteAsync(new UpdateStudentCommand
        {
            Id = studentId,
            FullName = request.FullName,
            Level = request.Level,
            ParentPhoneNumber = request.ParentPhoneNumber,
            StudentCode = request.StudentCode,
            PhoneNumber = request.PhoneNumber,
            SchoolName = request.SchoolName
        });

        return new()
        {
            Message = "Student updated successfully"
        };
    }
}
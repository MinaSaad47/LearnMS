using LearnMS.API.Common;
using LearnMS.API.Entities;
using LearnMS.API.Features.Administration.Contracts;
using LearnMS.API.Features.Assistants.Contracts;
using LearnMS.API.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.Administration;

[Route("api/administration")]
public sealed class AdministrationController : ControllerBase
{

    private readonly IAdministrationService _assistantsService;

    public AdministrationController(IAdministrationService assistantsService)
    {
        _assistantsService = assistantsService;
    }

    [HttpPost("teachers")]
    [ApiAuthorize(Role = UserRole.Teacher)]
    public async Task<ApiWrapper.Success<object?>> PostTeacher([FromBody] CreateTeacherRequest request)
    {
        await _assistantsService.ExecuteAsync(new CreateTeacherCommand
        {
            Email = request.Email,
            Password = request.Password,
        });

        return new()
        {
            Message = "Teacher Created"
        };
    }

    [HttpPost("assistants")]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageAssistants])]
    public async Task<ApiWrapper.Success<object?>> PostAssistant([FromBody] CreateAssistantRequest request)
    {
        await _assistantsService.ExecuteAsync(new CreateAssistantCommand
        {
            Email = request.Email,
            Password = request.Password,
        });

        return new()
        {
            Message = "Assistant Created"
        };
    }

    [HttpDelete("assistants/{assistantId:guid}")]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageAssistants])]
    public async Task<ApiWrapper.Success<object?>> DeleteAssistant(Guid assistantId)
    {
        await _assistantsService.ExecuteAsync(new DeleteAssistantCommand
        {
            Id = assistantId
        });

        return new()
        {
            Message = "Assistant deleted"
        };
    }

    [HttpPatch("assistants/{assistantId:guid}")]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageAssistants])]
    public async Task<ApiWrapper.Success<object?>> PatchAssistant([FromBody] UpdateAssistantRequest request, Guid assistantId)
    {
        await _assistantsService.ExecuteAsync(new UpdateAssistantCommand
        {
            Id = assistantId,
            Password = request.Password,
            Permissions = request.Permissions
        });

        return new()
        {
            Message = "Assistant updated"
        };
    }

    [HttpGet("assistants")]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageAssistants])]
    public async Task<ApiWrapper.Success<GetAssistantsResponse>> GetAssistants()
    {
        var result = await _assistantsService.QueryAsync(new GetAssistantsQuery());

        return new()
        {
            Data = new()
            {
                Items = result.Items
            },
            Message = result.Items.Count() > 0 ? "Successfully retrieved assistants" : "No assistants found"
        };
    }

    [HttpGet("permissions")]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageAssistants])]
    public async Task<ApiWrapper.Success<GetPermissionsResponse>> GetPermissions()
    {
        await Task.CompletedTask;
        return new()
        {
            Data = new()
            {
                Items = (Enum.GetValues(typeof(Permission)) as IEnumerable<Permission>)!.ToList()
            },
        };
    }
}
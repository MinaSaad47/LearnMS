using LearnMS.API.Common;
using LearnMS.API.Features.Administration.Contracts;
using LearnMS.API.Features.Assistants.Contracts;
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
    public async Task<ApiWrapper.Success<object?>> PostTeacher([FromBody] CreateTeacherCommand request)
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
    public async Task<ApiWrapper.Success<object?>> PostAssistant([FromBody] CreateAssistantCommand request)
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

    [HttpPatch("assistants/{assistantId:guid}")]
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
}
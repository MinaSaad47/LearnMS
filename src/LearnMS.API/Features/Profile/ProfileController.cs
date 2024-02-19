using System.Text.Json;
using LearnMS.API.Common;
using LearnMS.API.Entities;
using LearnMS.API.Features.Profile.Contracts;
using LearnMS.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.Profile;

[Route("api/profile")]
[Tags("Profile")]
public sealed class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly ICurrentUserService _currentUserService;

    public ProfileController(IProfileService profileService, ICurrentUserService currentUserService)
    {
        _profileService = profileService;
        _currentUserService = currentUserService;
    }

    [HttpPatch]
    [ApiAuthorize(Role = UserRole.Student)]
    public async Task<ApiWrapper.Success<object?>> Patch([FromBody] UpdateStudentProfileRequest request)
    {
        var user = await _currentUserService.GetUserAsync();

        if (user is null)
        {
            throw new ApiException(ProfileErrors.NotLoggedIn);
        }

        await _profileService.ExecuteAsync(new UpdateStudentProfileCommand
        {
            Id = user.Id,
            FullName = request.FullName,
            Level = request.Level,
            ParentPhoneNumber = request.ParentPhoneNumber,
            StudentCode = request.StudentCode,
            ProfilePicture = request.ProfilePicture,
            PhoneNumber = request.PhoneNumber,
            SchoolName = request.SchoolName,
        });

        return new()
        {
            Message = "Profile updated successfully"
        };
    }

    [HttpGet]
    public async Task<ApiWrapper.Success<GetProfileResponse>> Get()
    {
        var user = await _currentUserService.GetUserAsync();

        if (user is null)
        {
            throw new ApiException(ProfileErrors.NotLoggedIn);
        }

        var result = await _profileService.QueryAsync(new GetProfileQuery { Id = user.Id });


        if (result is null or { Account: null })
        {
            throw new ApiException(ProfileErrors.NotLoggedIn);
        }

        return new()
        {
            Data = new GetProfileResponse
            {
                Id = result.Account.Id,
                Email = result.Account.Email,
                FullName = result.FullName,
                Level = result.Level,
                ParentPhoneNumber = result.ParentPhoneNumber,
                StudentCode = result.StudentCode,
                PhoneNumber = result.PhoneNumber,
                School = result.School,
                Permissions = result.Account.User is Assistant ? ((Assistant)result.Account.User).Permissions.ToArray() : null,
                ProfilePicture = result.Account.ProfilePicture,
                Role = result.Account.User!.Role.ToString(),
                Credits = result.Account.User is Student ? ((Student)result.Account.User).Credit : null
            },
        };
    }
}
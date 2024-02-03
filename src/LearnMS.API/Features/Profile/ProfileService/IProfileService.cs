using LearnMS.API.Features.Auth;
using LearnMS.API.Features.Profile.Contracts;

namespace LearnMS.API.Features.Profile;

public interface IProfileService
{
    public Task ExecuteAsync(UpdateStudentProfileCommand command);

    public Task<GetProfileResult> QueryAsync(GetProfileQuery query);
}

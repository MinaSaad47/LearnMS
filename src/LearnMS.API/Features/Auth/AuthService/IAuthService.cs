using LearnMS.API.Features.Auth.Contracts;

namespace LearnMS.API.Features.Auth;

public interface IAuthService
{
    // Registration
    Task<RegisterResult> ExecuteAsync(RegisterStudentCommand command);
    Task<RegisterResult> ExecuteAsync(RegisterStudentExternalCommand command);

    // Login
    Task<LoginResult> ExecuteAsync(LoginCommand command);
    Task<LoginResult> ExecuteAsync(LoginExternalCommand command);
}

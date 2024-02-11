using LearnMS.API.Features.Auth.Contracts;

namespace LearnMS.API.Features.Auth;

public interface IAuthService
{
    // Registration
    Task<RegisterResult> ExecuteAsync(RegisterStudentCommand command);
    Task<RegisterResult> ExecuteAsync(RegisterStudentExternalCommand command);

    Task ExecuteAsync(VerifyEmailCommand command);
    
    Task ExecuteAsync(ForgotPasswordCommand command);
    Task ExecuteAsync(ResetPasswordCommand command);

    // Login
    Task<LoginResult> ExecuteAsync(LoginCommand command);
    Task<LoginResult> ExecuteAsync(LoginExternalCommand command);
}

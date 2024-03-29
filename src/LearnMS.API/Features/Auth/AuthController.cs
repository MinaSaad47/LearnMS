using LearnMS.API.Common;
using LearnMS.API.Features.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.Auth;

[Route("api/auth")]
[Tags("Auth")]
[ApiController]
public sealed class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }



    [HttpPost("students/register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ApiWrapper.Success<RegisterResult>> RegisterStudent([FromBody] RegisterStudentRequest request)
    {
        var result = await _authService.ExecuteAsync(new RegisterStudentCommand
        {
            Email = request.Email,
            FullName = request.FullName,
            Level = request.Level,
            ParentPhoneNumber = request.ParentPhoneNumber,
            StudentCode = request.StudentCode,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            School = request.School
        });

        Response.StatusCode = StatusCodes.Status201Created;

        return new()
        {
            Data = result,
            Message = "Student registered successfully, please verify your email"
        };
    }

    [HttpPost("students/register-external")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ApiWrapper.Success<RegisterResult>> RegisterStudentExternal([FromBody] RegisterStudentExternalCommand request)
    {
        var result = await _authService.ExecuteAsync(request);


        Response.StatusCode = StatusCodes.Status201Created;

        return new()
        {
            Data = result,
            Message = "Student registered successfully"
        };
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ApiWrapper.Success<LoginResult>> LoginStudent([FromBody] LoginCommand request)
    {
        var result = await _authService.ExecuteAsync(request);

        Response.StatusCode = StatusCodes.Status200OK;

        return new()
        {
            Data = result,
            Message = $"User logged in successfully"
        };
    }

    [HttpPost("login-external")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ApiWrapper.Success<LoginResult>> LoginStudentExternal([FromBody] LoginExternalCommand request)
    {
        var result = await _authService.ExecuteAsync(request);

        Response.StatusCode = StatusCodes.Status200OK;

        return new()
        {
            Data = result,
            Message = "Student logged in successfully"
        };
    }


    [HttpGet("verify-email")]
    public async Task VerifyEmail([FromQuery] string token)
    {
        await _authService.ExecuteAsync(new VerifyEmailCommand
        {
            Token = token
        });


        Response.Redirect("/sign-in-sign-up?email-verified=true");
    }

    [HttpPost("forgot-password")]
    public async Task<ApiWrapper.Success<object?>> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await _authService.ExecuteAsync(new ForgotPasswordCommand
        {
            Email = request.Email
        });

        return new()
        {
            Message = "Email sent successfully, please check your inbox",
        };
    }

    [HttpPost("reset-password")]
    public async Task<ApiWrapper.Success<object?>> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        await _authService.ExecuteAsync(new ResetPasswordCommand
        {
            Password = request.Password,
            Token = request.Token,
        });

        return new()
        {
            Message = "Password reset successfully"

        };
    }
}
using LearnMS.API.Common;
using LearnMS.API.Features.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.Auth;

[Route("api/auth")]
[Tags("Auth")]
public sealed class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }



    [HttpPost("students/register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ApiWrapper.Success<RegisterResult>> RegisterStudent([FromBody] RegisterStudentCommand request)
    {
        var result = await _authService.ExecuteAsync(request);

        Response.StatusCode = StatusCodes.Status201Created;

        return new()
        {
            Data = result,
            Message = "Student registered successfully"
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

}
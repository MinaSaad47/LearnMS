using LearnMS.API.Common;
using LearnMS.API.Features.CreditCodes.Contracts;
using LearnMS.API.Security;
using Microsoft.AspNetCore.Mvc;

namespace LearnMS.API.Features.CreditCodes;

[Route("api/credit-codes")]
[Tags("CreditCodes")]
public sealed class CreditCodesController : ControllerBase
{
    private readonly ICreditCodesService _creditCodesService;
    private readonly ICurrentUserService _currentUserService;

    public CreditCodesController(ICreditCodesService creditCodesService, ICurrentUserService currentUserService)
    {
        _creditCodesService = creditCodesService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<ApiWrapper.Success<GetCreditCodesResult>> Get()
    {
        return new()
        {
            Data = await _creditCodesService.QueryAsync(new GetCreditCodesQuery()),
            Message = "successfully retrieved credit codes"
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ApiAuthorize(Role = UserRole.Assistant)]
    public async Task<ApiWrapper.Success<object?>> Post([FromBody] GenerateCreditCodesCommand request)
    {
        Response.StatusCode = StatusCodes.Status201Created;
        await _creditCodesService.ExecuteAsync(request);
        return new()
        {
            Message = "successfully created credit codes"
        };

    }

    [HttpPut(Name = "redeem")]
    [ApiAuthorize(Role = UserRole.Student)]
    public async Task<ApiWrapper.Success<object?>> Redeem([FromQuery] string code)
    {
        var student = await _currentUserService.GetUserAsync();
        await _creditCodesService.ExecuteAsync(new RedeemCreditCodeCommand
        {
            Code = code,
            StudentId = student!.Id,
        });

        return new()
        {
            Message = "successfully redeemed credit code"
        };
    }
}
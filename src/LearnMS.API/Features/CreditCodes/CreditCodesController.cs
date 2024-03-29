using LearnMS.API.Common;
using LearnMS.API.Entities;
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
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageCreditCodes, Permission.GenerateCreditCodes])]
    public async Task<ApiWrapper.Success<PageList<SingleCreditCodeItem>>> Get(int? page, int? pageSize, string? search, string? sortOrder)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        bool canManage;

        if (currentUser!.Role == UserRole.Student)
        {
            canManage = true;
        }
        else
        {
            canManage = currentUser.Permissions.Contains(Permission.ManageCreditCodes);
        }


        var result = await _creditCodesService.QueryAsync(new GetCreditCodesQuery
        {
            Page = page,
            PageSize = pageSize,
            Search = search,
            SortOrder = sortOrder,
            GeneratorId = canManage ? null : currentUser!.Role == UserRole.Assistant ? currentUser.Id : null
        });
        return new()
        {
            Data = result,
            Message = "successfully retrieved credit codes"
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageCreditCodes, Permission.GenerateCreditCodes])]
    public async Task<ApiWrapper.Success<object?>> Post([FromBody] GenerateCreditCodesRequest request)
    {
        var currentUser = await _currentUserService.GetUserAsync();

        await _creditCodesService.ExecuteAsync(new GenerateCreditCodesCommand
        {
            Count = request.Count,
            Value = request.Value,
            GeneratorId = currentUser!.Role == UserRole.Assistant ? currentUser.Id : null,
        });
        Response.StatusCode = StatusCodes.Status201Created;
        return new()
        {
            Message = "successfully created credit codes"
        };

    }

    [HttpPut("redeem")]
    [ApiAuthorize(Role = UserRole.Student)]
    public async Task<ApiWrapper.Success<RedeemCreditCodeResponse>> Redeem([FromQuery] string code)
    {
        var student = await _currentUserService.GetUserAsync();
        var result = await _creditCodesService.ExecuteAsync(new RedeemCreditCodeCommand
        {
            Code = code,
            StudentId = student!.Id,
        });

        return new()
        {
            Message = "successfully redeemed credit code",
            Data = new()
            {
                Value = result.Value
            }
        };
    }

    [HttpPost("sell")]
    [ApiAuthorize(Role = UserRole.Assistant, Permissions = [Permission.ManageCreditCodes, Permission.GenerateCreditCodes])]
    public async Task<ApiWrapper.Success<SellCreditCodesResponse>> Sell([FromBody] SellCreditCodesRequest request)
    {

        var currentUser = await _currentUserService.GetUserAsync();

        var result = await _creditCodesService.ExecuteAsync(new SellCreditCodesCommand
        {
            Codes = request.Codes,
            SellerId = currentUser!.Role == UserRole.Assistant ? currentUser.Id : null,
        });

        return new()
        {
            Message = $"Sold {result.CreditCodes.Count} Credit Code Successfully",
            Data = new()
            {
                CreditCodes = result.CreditCodes
            }
        };
    }
}
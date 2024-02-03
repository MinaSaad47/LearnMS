using LearnMS.API.Common;

namespace LearnMS.API.Features.CreditCodes;

public static class CreditCodesErrors
{
    public static readonly ApiError AlreadyRedeemed = new("credit-code/already-redeemed", "Credit code already redeemed", StatusCodes.Status400BadRequest);
    public static readonly ApiError InvalidCode = new("credit-code/invalid-code", "Invalid credit code", StatusCodes.Status400BadRequest);

}
using LearnMS.API.Features.CreditCodes.Contracts;

namespace LearnMS.API.Features.CreditCodes;

public interface ICreditCodesService
{
    Task ExecuteAsync(GenerateCreditCodesCommand request);
    Task ExecuteAsync(RedeemCreditCodeCommand request);

    Task<GetCreditCodesResult> QueryAsync(GetCreditCodesQuery request);

}

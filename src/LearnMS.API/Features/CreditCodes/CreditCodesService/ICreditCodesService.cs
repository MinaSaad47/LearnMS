using LearnMS.API.Features.CreditCodes.Contracts;

namespace LearnMS.API.Features.CreditCodes;

public interface ICreditCodesService
{
    Task ExecuteAsync(GenerateCreditCodesCommand request);
    Task<RedeemCreditCodeResult> ExecuteAsync(RedeemCreditCodeCommand request);

    Task<PageList<SingleCreditCodeItem>> QueryAsync(GetCreditCodesQuery request);

    Task<SellCreditCodesResult> ExecuteAsync(SellCreditCodesCommand request);

}

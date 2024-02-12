using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.CreditCodes.Contracts;
using LearnMS.API.Features.Students;
using Microsoft.EntityFrameworkCore;

namespace LearnMS.API.Features.CreditCodes;

public class CreditCodesService : ICreditCodesService
{
    private readonly AppDbContext _dbContext;
    private readonly ICodeGenerator _codeGenerator;

    public CreditCodesService(AppDbContext dbContext, ICodeGenerator codeGenerator)
    {
        _dbContext = dbContext;
        _codeGenerator = codeGenerator;
    }

    public async Task ExecuteAsync(GenerateCreditCodesCommand request)
    {
        List<CreditCode> creditCodes = new();
        for (int i = 0; i < request.Count; i++)
        {
            var code = await _codeGenerator.GenerateAsync(15, async (code) =>
            {
                return await _dbContext.CreditCodes.CountAsync(x => x.Code == code) == 0;
            });

            CreditCode creditCode = new()
            {
                Code = code,
                Value = request.Value,
                GeneratorId = request.GeneratorId,
            };
            creditCodes.Add(creditCode);
        }
        await _dbContext.CreditCodes.AddRangeAsync(creditCodes);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RedeemCreditCodeResult> ExecuteAsync(RedeemCreditCodeCommand request)
    {

        var creditCode = await _dbContext.CreditCodes.FirstOrDefaultAsync(x => x.Code == request.Code);

        if (creditCode is null)
        {
            throw new ApiException(CreditCodesErrors.InvalidCode);
        }

        var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == request.StudentId);

        if (student is null)
        {
            throw new ApiException(StudentsErrors.NotFound);
        }

        if (creditCode.StudentId is not null)
        {
            throw new ApiException(CreditCodesErrors.AlreadyRedeemed);
        }

        student.RedeemCode(creditCode, out var redeemedCode);
        _dbContext.Update(redeemedCode);

        _dbContext.Update(student);

        await _dbContext.SaveChangesAsync();

        return new()
        {
            Value = creditCode.Value
        };
    }

    public async Task<SellCreditCodesResult> ExecuteAsync(SellCreditCodesCommand request)
    {
        List<CreditCode> soldCodes = new();

        foreach (var toBeSold in request.Codes)
        {
            try
            {
                var code = await _dbContext.CreditCodes.Where(x => x.Code == toBeSold && x.Status == CreditCodeStatus.Fresh).FirstOrDefaultAsync();
                if (code is null) continue;
                code.SellerId = request.SellerId;
                _dbContext.Update(code);
                await _dbContext.SaveChangesAsync();
                soldCodes.Add(code);
            }
            catch (Exception)
            {
            }
        }

        return new()
        {
            CreditCodes = soldCodes
        };
    }

    public async Task<PageList<SingleCreditCodeItem>> QueryAsync(GetCreditCodesQuery query)
    {

        CreditCodeStatus? status = null;


        var search = query.Search?.ToLower() ?? "";

        if (search == "redeemed")
        {
            status = CreditCodeStatus.Redeemed;
        }
        else if (search == "sold")
        {
            status = CreditCodeStatus.Sold;
        }
        else if (search == "fresh")
        {
            status = CreditCodeStatus.Fresh;
        }


        var creditCodesQuery = from code in _dbContext.CreditCodes
                               join redeemerAccount in _dbContext.Accounts on code.StudentId equals redeemerAccount.Id into redeemers
                               from redeemer in redeemers.DefaultIfEmpty()
                               join generatorAccount in _dbContext.Accounts on code.GeneratorId equals generatorAccount.Id into generators
                               from generator in generators.DefaultIfEmpty()
                               join sellerAccount in _dbContext.Accounts on code.SellerId equals sellerAccount.Id into sellers
                               from seller in sellers.DefaultIfEmpty()
                               orderby query.SortOrder == "asc" ? code.Status : 0
                               orderby query.SortOrder != "desc" ? 0 : code.Status descending
                               where status != null ? code.Status == status : true &&
                               query.GeneratorId != null ? code.GeneratorId == query.GeneratorId : true
                               select new SingleCreditCodeItem
                               {
                                   Code = code.Code,
                                   Value = code.Value,
                                   GeneratedAt = code.GeneratedAt,
                                   Status = code.Status,
                                   Redeemer = redeemer != null && code.RedeemedAt != null ? new CreditCodeRedeemer
                                   {
                                       Id = redeemer.Id,
                                       Email = redeemer.Email,
                                       RedeemedAt = code.RedeemedAt!.Value,
                                   } : null,
                                   Generator = generator != null ? new CreditCodeGenerator
                                   {
                                       Id = generator.Id,
                                       Email = generator.Email
                                   } : null,
                                   SoldAt = code.SoldAt,
                                   Seller = seller != null && code.SoldAt != null ? new CreditCodeSeller
                                   {
                                       Id = seller.Id,
                                       Email = seller.Email,
                                   } : null
                               }
                               into result
                               select result;

        if (!string.IsNullOrEmpty(query.Search) && status is null)
        {
            creditCodesQuery = creditCodesQuery.Where(x => x.Code.Contains(query.Search));
        }


        return await PageList<SingleCreditCodeItem>.CreateAsync(creditCodesQuery, query.Page ?? 1, query.PageSize ?? 10);
    }

}

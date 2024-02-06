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

        creditCode.StudentId = request.StudentId;
        student.Credit += creditCode.Value;

        await _dbContext.SaveChangesAsync();

        return new()
        {
            Value = creditCode.Value
        };
    }

    public async Task<PageList<SingleCreditCodeItem>> QueryAsync(GetCreditCodesQuery query)
    {

        var creditCodesQuery = from code in _dbContext.CreditCodes
                               join redeemerAccount in _dbContext.Accounts on code.StudentId equals redeemerAccount.Id into redeemers
                               from redeemer in redeemers.DefaultIfEmpty()
                               join generatorAccount in _dbContext.Accounts on code.AssistantId equals generatorAccount.Id into generators
                               from generator in generators.DefaultIfEmpty()
                               orderby query.SortOrder != "desc" ? code.Status : 0
                               orderby query.SortOrder != "desc" ? 0 : code.Status descending
                               select new SingleCreditCodeItem
                               {
                                   Code = code.Code,
                                   Value = code.Value,
                                   Status = code.Status.ToString(),
                                   Redeemer = redeemer != null ? new CreditCodeRedeemer
                                   {
                                       Id = redeemer.Id,
                                       Email = redeemer.Email
                                   } : null,
                                   Generator = generator != null ? new CreditCodeGenerator
                                   {
                                       Id = generator.Id,
                                       Email = generator.Email
                                   } : null
                               }
                               into result
                               select result;

        if (!string.IsNullOrEmpty(query.Search))
        {
            creditCodesQuery = creditCodesQuery.Where(x => x.Code.Contains(query.Search));
        }


        return await PageList<SingleCreditCodeItem>.CreateAsync(creditCodesQuery, query.Page ?? 1, query.PageSize ?? 10);
    }

}

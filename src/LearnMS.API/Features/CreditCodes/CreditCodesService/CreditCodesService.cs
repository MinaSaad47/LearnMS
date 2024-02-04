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

    public async Task<GetCreditCodesResult> QueryAsync(GetCreditCodesQuery request)
    {
        var items = await _dbContext.CreditCodes.ToListAsync();
        return new(items);
    }
}

using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.Administration.Contracts;
using LearnMS.API.Features.Assistants.Contracts;
using LearnMS.API.Security.PasswordHasher;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;

namespace LearnMS.API.Features.Administration;

public sealed class AdministrationService : IAdministrationService
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public AdministrationService(AppDbContext dbContext, IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task ExecuteAsync(UpdateAssistantCommand command)
    {
        var assistant = await _dbContext.Assistants.Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Id == command.Id);

        if (assistant is null)
        {
            throw new ApiException(AdministrationErrors.AssistantNotFound);
        }

        if (command.Permissions is not null)
        {
            assistant.Permissions = command.Permissions.ToHashSet();
        }

        if (!string.IsNullOrWhiteSpace(command.Password))
        {
            var passwordHash = _passwordHasher.Hash(command.Password);
            assistant.Accounts.Where(x => x.ProviderType == ProviderType.Local).ToList().ForEach(x =>
            {
                x.PasswordHash = passwordHash;
            });
        }
        _dbContext.Assistants.Update(assistant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(CreateTeacherCommand command)
    {
        var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(x => x.Email == command.Email);

        if (account != null)
        {
            throw new ApiException(AdministrationErrors.EmailAlreadyRegistered);
        }

        var teacher = Teacher.Register(new Account
        {
            Email = command.Email,
            PasswordHash = _passwordHasher.Hash(command.Password),
            ProviderType = ProviderType.Local,
            VerifiedAt = DateTime.UtcNow
        });

        await _dbContext.Teachers.AddAsync(teacher);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(CreateAssistantCommand command)
    {
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(x => x.Email == command.Email);

        if (account != null)
        {
            throw new ApiException(AdministrationErrors.EmailAlreadyRegistered);
        }

        var passwordHash = _passwordHasher.Hash(command.Password);

        var assistant = Assistant.Register(new Account
        {
            Email = command.Email,
            VerifiedAt = DateTime.UtcNow,
            PasswordHash = passwordHash,
            ProviderType = ProviderType.Local,
        });

        assistant.Permissions.Add(Permission.ManageCourses);

        await _dbContext.Assistants.AddAsync(assistant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(DeleteAssistantCommand command)
    {

        var assistant = await _dbContext.Assistants
            .Include(x => x.Accounts)
            .FirstOrDefaultAsync(x => x.Id == command.Id);


        if (assistant is null)
        {
            throw new ApiException(AdministrationErrors.AssistantNotFound);
        }


        _dbContext.Assistants.Remove(assistant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(ClaimAssistantIncomesCommand command)
    {
        await _dbContext.Set<CreditCode>().Where(x => x.SellerId == command.AssistantId).ExecuteUpdateAsync(x => x.SetProperty(x => x.ClaimedAt, x => DateTime.UtcNow));
        await _dbContext.Set<StudentCredit>().Where(x => x.AssistantId == command.AssistantId).ExecuteUpdateAsync(x => x.SetProperty(x => x.ClaimedAt, x => DateTime.UtcNow));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<GetAssistantsResult> QueryAsync(GetAssistantsQuery query)
    {
        var result = from assistants in _dbContext.Set<Assistant>()
                     join accounts in _dbContext.Set<Account>() on assistants.Id equals accounts.Id
                     select new SingleAssistant
                     {
                         Id = accounts.Id,
                         Email = accounts.Email,
                         Permissions = assistants.Permissions.ToList()
                     };

        return new GetAssistantsResult
        {
            Items = await result.ToListAsync()
        };
    }

    public async Task<GetAssistantIncomeResult> QueryAsync(GetAssistantIncomeQuery query)
    {
        var assistant = await _dbContext.Assistants
            .Include(x => x.Accounts)
            .FirstOrDefaultAsync(x => x.Id == query.AssistantId) ?? throw new ApiException(AdministrationErrors.AssistantNotFound);

        var creditCodes = _dbContext.Set<CreditCode>().Where(x => x.SellerId == assistant.Id).Select(x => new
        {
            Amount = x.Value,
            StudentId = string.Empty,
            x.Code,
            HappenedAt = x.SoldAt!.Value,
            x.ClaimedAt,
        });

        var studentCredits = _dbContext.Set<StudentCredit>().Where(x => x.AssistantId == assistant.Id).Select(x => new
        {
            Amount = x.Value,
            StudentId = x.StudentId.ToString(),
            Code = string.Empty,
            HappenedAt = x.CreditedAt,
            x.ClaimedAt,
        });


        var result = creditCodes.Union(studentCredits).OrderByDescending(x => x.HappenedAt);

        var combined = result.Select(x => new SingleAssistantIncome
        {
            Amount = x.Amount,
            Code = x.Code != string.Empty ? x.Code : null,
            HappenedAt = x.HappenedAt,
            StudentId = x.StudentId != string.Empty ? Guid.Parse(x.StudentId) : null,
            ClaimedAt = x.ClaimedAt
        });


        var totalIncome = await _dbContext.Set<CreditCode>().Where(x => x.SellerId == assistant.Id).SumAsync(x => x.Value) +
            await _dbContext.Set<StudentCredit>().Where(x => x.AssistantId == assistant.Id).SumAsync(x => x.Value);

        var unclaimedIncome = await _dbContext.Set<StudentCredit>().Where(x => x.AssistantId == assistant.Id && x.ClaimedAt == null).SumAsync(x => x.Value) +
            await _dbContext.Set<CreditCode>().Where(x => x.SellerId == assistant.Id && x.ClaimedAt == null).SumAsync(x => x.Value);

        return new GetAssistantIncomeResult
        {
            Data = await PageList<SingleAssistantIncome>.CreateAsync(combined, query.Page ?? 1, query.PageSize ?? 10),
            TotalIncome = totalIncome,
            UnClaimedIncome = unclaimedIncome,
        };
    }

    public async Task<GetAssistantResult> QueryAsync(GetAssistantQuery query)
    {
        var assistant = await _dbContext.Assistants.Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Id == query.Id) ?? throw new ApiException(AdministrationErrors.AssistantNotFound);


        return new()
        {
            Id = assistant.Id,
            Email = assistant.Accounts.FirstOrDefault()!.Email,
            Permissions = assistant.Permissions.ToList()
        };
    }
}


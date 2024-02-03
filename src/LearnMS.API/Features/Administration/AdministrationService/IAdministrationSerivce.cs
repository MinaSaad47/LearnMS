using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.Administration.Contracts;
using LearnMS.API.Features.Assistants.Contracts;
using LearnMS.API.Security.PasswordHasher;
using Microsoft.EntityFrameworkCore;

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
        var account = new Account
        {
            Email = command.Email,
            PasswordHash = _passwordHasher.Hash(command.Password),
            ProviderType = ProviderType.Local,
        };

        var teacher = Teacher.Register(account);

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
            PasswordHash = passwordHash,
            ProviderType = ProviderType.Local,
        });

        assistant.Permissions.Add(Permission.ManageCourses);

        await _dbContext.Assistants.AddAsync(assistant);
        await _dbContext.SaveChangesAsync();
    }
}

public interface IAdministrationService
{

    public Task ExecuteAsync(UpdateAssistantCommand command);
    public Task ExecuteAsync(CreateAssistantCommand command);
    public Task ExecuteAsync(CreateTeacherCommand command);

}


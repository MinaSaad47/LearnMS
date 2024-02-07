using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.Profile.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LearnMS.API.Features.Profile;

public sealed class ProfileService : IProfileService
{
    private readonly AppDbContext _dbContext;

    public ProfileService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsync(UpdateStudentProfileCommand command)
    {
        var profile = await _dbContext.Accounts.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == command.Id);

        if (profile is null)
        {
            return;
        }

        if (profile.User is not Student)
        {
            throw new ApiException(ProfileErrors.NotLoggedIn);
        }

        var student = (Student)profile.User;

        if (!string.IsNullOrWhiteSpace(command.FullName))
        {
            student.FullName = command.FullName;
        }

        if (!string.IsNullOrWhiteSpace(command.PhoneNumber))
        {
            student.PhoneNumber = command.PhoneNumber;
        }

        if (!string.IsNullOrWhiteSpace(command.ParentPhoneNumber))
        {
            student.PhoneNumber = command.ParentPhoneNumber;
        }

        if (!string.IsNullOrWhiteSpace(command.ProfilePicture))
        {
            profile.ProfilePicture = command.ProfilePicture;
        }

        if (command.Level.HasValue)
        {
            student.Level = command.Level.Value;
        }

        _dbContext.Update(profile);

        await _dbContext.SaveChangesAsync();

    }

    public async Task<GetProfileResult> QueryAsync(GetProfileQuery query)
    {
        var profile = await _dbContext.Accounts.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == query.Id);

        return new()
        {
            Account = profile
        };
    }
}

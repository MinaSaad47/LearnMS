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

        if (!string.IsNullOrWhiteSpace(command.StudentCode))
        {
            student.PhoneNumber = command.StudentCode;
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

    public async Task<GetProfileResult?> QueryAsync(GetProfileQuery query)
    {
        // var profile = await _dbContext.Accounts.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == query.Id);

        var result = from accounts in _dbContext.Accounts
                     join students in _dbContext.Students on accounts.Id equals students.Id into groupedStudents
                     from gs in groupedStudents.DefaultIfEmpty()
                     where accounts.Id == query.Id
                     select new GetProfileResult
                     {
                         Account = accounts,
                         FullName = gs != null ? gs.FullName : null,
                         Level = gs != null ? gs.Level : null,
                         ParentPhoneNumber = gs != null ? gs.ParentPhoneNumber : null,
                         StudentCode = gs != null ? gs.StudentCode : null,
                         PhoneNumber = gs != null ? gs.PhoneNumber : null,
                         School = gs != null ? gs.SchoolName : null,
                     };


        return await result.FirstOrDefaultAsync();
    }
}

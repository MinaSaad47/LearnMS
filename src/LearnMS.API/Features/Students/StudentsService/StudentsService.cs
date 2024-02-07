using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.Auth;
using LearnMS.API.Features.Students.Contracts;
using LearnMS.API.Security.PasswordHasher;
using Microsoft.EntityFrameworkCore;

namespace LearnMS.API.Features.Students;

public sealed class StudentsService(AppDbContext db, IPasswordHasher passwordHasher) : IStudentsService
{
    public async Task ExecuteAsync(CreateStudentCommand command)
    {
        var account = await db.Accounts
            .FirstOrDefaultAsync(x => x.Email.ToLower() == command.Email.ToLower());

        if (account != null)
        {
            throw new ApiException(AuthErrors.EmailAlreadyExists);
        }

        var passwordHash = passwordHasher.Hash(command.Password);

        var student = Student.Register(new Account
        {
            Email = command.Email.ToLower(),
            VerifiedAt = DateTime.UtcNow,
            PasswordHash = passwordHash,
            ProviderType = ProviderType.Local,
        }, command.FullName, command.PhoneNumber, command.ParentPhoneNumber, command.School, command.Level);


        await db.Students.AddAsync(student);
        await db.SaveChangesAsync();
    }

    public async Task ExecuteAsync(AddStudentCreditCommand command)
    {
        var student = await db.Students.FirstOrDefaultAsync(x => x.Id == command.Id);

        if (student is null)
        {
            throw new ApiException(StudentsErrors.NotFound);
        }

        student.Credit += command.Amount;

        db.Update(student);
        await db.SaveChangesAsync();
    }

    public async Task<PageList<SingleStudent>> QueryAsync(GetStudentsQuery query)
    {
        var result = from students in db.Set<Student>()
                     join accounts in db.Set<Account>() on students.Id equals accounts.Id
                     where
                        query.Search != null ? (students.FullName.ToLower().Contains(query.Search) || accounts.Email.ToLower().Contains(query.Search)) : true
                     select new SingleStudent
                     {
                         Id = students.Id,
                         Email = accounts.Email,
                         Credit = students.Credit,
                         FullName = students.FullName,
                         Level = students.Level,
                         IsVerified = accounts.VerifiedAt != null,
                         ParentPhoneNumber = students.ParentPhoneNumber,
                         PhoneNumber = students.PhoneNumber,
                         ProfilePicture = accounts.ProfilePicture,
                         SchoolName = students.SchoolName
                     };

        return await PageList<SingleStudent>.CreateAsync(result, query.Page ?? 1, query.PageSize ?? 10);
    }
}
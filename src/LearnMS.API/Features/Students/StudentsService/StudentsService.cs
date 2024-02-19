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
        }, command.FullName, command.PhoneNumber, command.ParentPhoneNumber, command.StudentCode, command.School, command.Level);


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

        student.AddCredit(command.AssistantId, command.Amount, out var studentCredit);


        await db.AddAsync(studentCredit);
        db.Update(student);
        await db.SaveChangesAsync();
    }

    public async Task ExecuteAsync(DeleteStudentCommand command)
    {
        var student = await db.Students.FirstOrDefaultAsync(x => x.Id == command.Id);

        if (student is null)
        {
            throw new ApiException(StudentsErrors.NotFound);
        }

        db.Remove(student);
        await db.SaveChangesAsync();
    }

    public async Task ExecuteAsync(UpdateStudentCommand command)
    {
        var student = await db.Students.FirstOrDefaultAsync(x => x.Id == command.Id) ?? throw new ApiException(StudentsErrors.NotFound);


        if (!string.IsNullOrEmpty(command.FullName))
        {
            student.FullName = command.FullName;
        }

        if (!string.IsNullOrEmpty(command.PhoneNumber))
        {
            student.PhoneNumber = command.PhoneNumber;
        }

        if (!string.IsNullOrEmpty(command.ParentPhoneNumber))
        {
            student.ParentPhoneNumber = command.ParentPhoneNumber;
        }
         if (!string.IsNullOrEmpty(command.StudentCode))
        {
            student.StudentCode = command.StudentCode;
        }

        if (!string.IsNullOrEmpty(command.SchoolName))
        {
            student.SchoolName = command.SchoolName;
        }

        if (command.Level is not null)
        {
            student.Level = command.Level.Value;
        }


        db.Update(student);
        await db.SaveChangesAsync();

    }

    public async Task<PageList<SingleStudent>> QueryAsync(GetStudentsQuery query)
    {
        var result = from students in db.Set<Student>()
                     join accounts in db.Set<Account>() on students.Id equals accounts.Id
                     orderby students.FullName
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
                         StudentCode = students.StudentCode,
                         PhoneNumber = students.PhoneNumber,
                         ProfilePicture = accounts.ProfilePicture,
                         SchoolName = students.SchoolName
                     };

        return await PageList<SingleStudent>.CreateAsync(result, query.Page ?? 1, query.PageSize ?? 10);
    }

    public async Task<GetStudentResult> QueryAsync(GetStudentQuery query)
    {
        var result = from students in db.Set<Student>()
                     join accounts in db.Set<Account>() on students.Id equals accounts.Id
                     where students.Id == query.Id
                     select new GetStudentResult
                     {
                         ProfilePicture = accounts.ProfilePicture,
                         FullName = students.FullName,
                         Level = students.Level,
                         ParentPhoneNumber = students.ParentPhoneNumber,
                         StudentCode = students.StudentCode,
                         PhoneNumber = students.PhoneNumber,
                         SchoolName = students.SchoolName,
                         Credit = students.Credit
                         ,
                         Email = accounts.Email,
                         Id = students.Id,
                         IsVerified = accounts.VerifiedAt != null
                     };

        var student = await result.FirstOrDefaultAsync() ?? throw new ApiException(StudentsErrors.NotFound);

        return student;
    }
}
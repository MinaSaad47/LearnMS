using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnMS.API.Entities;

[JsonConverter(typeof(StringEnumConverter))]
public enum StudentLevel
{
    Level0,
    Level1,
    Level2,
    Level3
}

public class Student : User
{

    public required string FullName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string ParentPhoneNumber { get; set; }
    public required string SchoolName { get; set; }
    public required StudentLevel Level { get; set; }

    public decimal Credit { get; set; } = 0;


    public static Student Register(Account account, string fullName, string phoneNumber, string parentPhoneNumber, string schoolName, StudentLevel level)
    {
        var id = Guid.NewGuid();

        account.Id = id;

        return new Student
        {
            Id = id,
            FullName = fullName,
            PhoneNumber = phoneNumber,
            ParentPhoneNumber = parentPhoneNumber,
            Level = level,
            SchoolName = schoolName,
            Accounts = new List<Account>() {
               account
            }
        };
    }

    private Student() { }
}
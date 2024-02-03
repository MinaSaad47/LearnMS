using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace LearnMS.API.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StudentLevel
{
    Level1,
    Level2,
    Level3
}

public class Student : User
{

    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ParentPhoneNumber { get; set; }
    public string? SchoolName { get; set; }
    public StudentLevel Level { get; set; }

    public decimal Credit { get; set; } = 0;


    public static Student Register(Account account)
    {
        var id = Guid.NewGuid();

        account.Id = id;

        return new Student
        {
            Id = id,
            Accounts = new List<Account>() {
               account
            }
        };
    }

    private Student() { }
}
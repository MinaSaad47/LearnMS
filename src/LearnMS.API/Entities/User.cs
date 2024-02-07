using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using LearnMS.API.Entities;
using Newtonsoft.Json.Converters;

public abstract class User
{
    public Guid Id { get; set; }
    public ICollection<Account> Accounts { get; set; } = new List<Account>();

    public UserRole Role
    {
        get
        {
            return this switch
            {
                Assistant => UserRole.Assistant,
                Student => UserRole.Student,
                Teacher => UserRole.Teacher,
                _ => throw new UnreachableException(),
            };
        }
    }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum UserRole
{
    Teacher,
    Assistant,
    Student
}
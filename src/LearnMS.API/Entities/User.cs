using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using LearnMS.API.Entities;

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

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    [EnumMember(Value = "Teacher")]
    Teacher,
    [EnumMember(Value = "Assistant")]
    Assistant,
    [EnumMember(Value = "Student")]
    Student
}
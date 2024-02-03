using System.Security;
using Microsoft.AspNetCore.Identity;

namespace LearnMS.API.Entities;

public class Assistant : User
{

    public static Assistant Register(Account account)
    {
        var id = Guid.NewGuid();
        account.Id = id;
        return new Assistant
        {
            Id = id,
            Accounts = new List<Account> { account }
        };
    }

    public HashSet<Permission> Permissions { get; set; } = new();

    private Assistant() { }
}
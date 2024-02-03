namespace LearnMS.API.Entities;

public sealed class Teacher : User
{

    public static Teacher Register(Account account)
    {
        var id = Guid.NewGuid();

        account.Id = id;

        return new Teacher
        {
            Id = id,
            Accounts = new List<Account>() {
               account
            }
        };
    }
}
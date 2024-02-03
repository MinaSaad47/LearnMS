using LearnMS.API.Entities;

namespace LearnMS.API.Security;

public interface ICurrentUserService
{
    Task<CurrentUser?> GetUserAsync();
}
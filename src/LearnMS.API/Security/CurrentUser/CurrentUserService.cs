using System.Security.Claims;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnMS.API.Security;

public sealed class CurrentUserService(IServiceProvider _serviceProvider,
                                       AppDbContext _dbContext) : ICurrentUserService
{


    public async Task<CurrentUser?> GetUserAsync()
    {
        var _httpContext = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext!;

        if (_httpContext.User.Identity is null or { IsAuthenticated: false })
        {
            return null;
        }

        var userId = _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            return null;
        }

        if (!Guid.TryParse(userId, out var accountId))
        {
            return null;
        }

        var account = await _dbContext.Accounts.Include(x => x.User).FirstOrDefaultAsync(a => a.Id == Guid.Parse(userId));

        if (account is null)
        {
            return null;
        }

        return new CurrentUser
        {
            Id = account.Id,
            Role = account.User.Role,
            Permissions = (account.User as Assistant)?.Permissions ?? []
        };
    }
}
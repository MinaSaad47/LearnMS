using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Entities;
using LearnMS.API.Features.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearnMS.API.Security;

public class ApiAuthorizeAttribute : ActionFilterAttribute, IAsyncAuthorizationFilter
{
    public UserRole Role = UserRole.Teacher;
    public Permission[]? Permissions;



    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        var currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();
        var user = await currentUserService.GetUserAsync();

        if (user is null)
        {
            throw new ApiException(AuthErrors.Unauthorized);
        }

        if (user.Role is UserRole.Teacher && Role is not UserRole.Student)
        {
            return;
        }

        if (user.Role is UserRole.Assistant && Role is not UserRole.Student)
        {
            if (Permissions is null || Permissions.All(p => user.Permissions.Contains(p)))
            {
                return;
            }
        }

        if (user.Role is UserRole.Student && Role is UserRole.Student)
        {
            return;
        }

        throw new ApiException(AuthErrors.Forbidden);
    }
}
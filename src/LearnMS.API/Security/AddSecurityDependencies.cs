namespace LearnMS.API.Security;

public static class AddSecurityDependencies
{
    public static void AddSecurityServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}
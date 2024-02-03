using LearnMS.API.Features.Administration;
using LearnMS.API.Features.Auth;
using LearnMS.API.Features.Courses;
using LearnMS.API.Features.Courses.Lectures;
using LearnMS.API.Features.Courses.Lectures.Lessons;
using LearnMS.API.Features.CreditCodes;
using LearnMS.API.Features.Profile;
using LearnMS.API.Security.PasswordHasher;

namespace LearnMS.API.Features;

public static class AddFeaturesDependencies
{
    public static IServiceCollection AddFeaturesServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<ICodeGenerator, CodeGenerator>();
        services.AddScoped<ICreditCodesService, CreditCodesService>();

        services.AddScoped<IProfileService, ProfileService>();


        // services.AddScoped<ILecturesService>();
        // services.AddScoped<ILessonsService>();
        services.AddScoped<ICoursesService, CoursesService>();

        services.AddScoped<IAdministrationService, AdministrationService>();

        return services;
    }
}
using LearnMS.API.Common;

namespace LearnMS.API.Features.Profile;

public static class ProfileErrors
{
    public static readonly ApiError NotLoggedIn = new("profile/not-logged-in", "You are not logged in.", StatusCodes.Status401Unauthorized);
}
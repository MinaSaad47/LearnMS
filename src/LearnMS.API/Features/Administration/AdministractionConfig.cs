using LearnMS.API.Entities;

namespace LearnMS.API.Features.Administration;

public sealed class AdministrationConfig
{
    public const string Section = "Administration";

    public List<TeacherConfig>? Teachers { get; init; }
    public List<AssistantConfig>? Assistants { get; init; }
}

public sealed class TeacherConfig
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public sealed class AssistantConfig
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
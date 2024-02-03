using LearnMS.API.Entities;

namespace LearnMS.API.Features.Assistants.Contracts;



public class UpdateAssistantCommand
{
    public required Guid Id { get; init; }
    public string? Password { get; init; }
    public List<Permission>? Permissions { get; init; }
}

public class UpdateAssistantRequest
{
    public string? Password { get; init; }
    public List<Permission>? Permissions { get; init; }
}
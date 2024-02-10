using System.ComponentModel.DataAnnotations;
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
    [MinLength(8)]
    public string? Password { get; init; }
    public List<Permission>? Permissions { get; init; }
}
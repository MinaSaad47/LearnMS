using System.Text.Json.Serialization;
using LearnMS.API.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnMS.API.Features.Administration.Contracts;

public sealed record GetAssistantsQuery;
public sealed record GetAssistantsResult
{
    public required List<SingleAssistant> Items { get; init; }
};

public sealed record GetAssistantsResponse
{
    public required List<SingleAssistant> Items { get; init; }
};

public sealed record SingleAssistant
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }

    [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
    public required List<Permission> Permissions { get; init; }
}
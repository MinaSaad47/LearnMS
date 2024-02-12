namespace LearnMS.API.Features.Administration.Contracts;

using LearnMS.API.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public sealed record GetAssistantQuery
{
    public Guid Id { get; set; }
}

public sealed record GetAssistantResult
{

    public required Guid Id { get; init; }
    public required string Email { get; init; }

    [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
    public required List<Permission> Permissions { get; init; }
}

public sealed record GetAssistantResponse
{

    public required Guid Id { get; init; }
    public required string Email { get; init; }

    [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
    public required List<Permission> Permissions { get; init; }
}
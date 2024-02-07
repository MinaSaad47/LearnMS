using LearnMS.API.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnMS.API.Features.Administration.Contracts;

public sealed record GetPermissionsResponse
{
    [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
    public required List<Permission> Items { get; init; }
}
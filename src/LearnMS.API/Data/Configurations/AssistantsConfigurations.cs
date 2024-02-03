using System.Text.Json;
using System.Text.Json.Serialization;
using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class AssistantsConfigurations : IEntityTypeConfiguration<Assistant>
{
    public void Configure(EntityTypeBuilder<Assistant> builder)
    {
        builder.HasBaseType<User>();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
        };


        var valueComparer = new ValueComparer<HashSet<Permission>>(
            (c1, c2) => c1 != null && c2 != null ? c1.SequenceEqual(c2) : true,
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToHashSet()
        );

        builder.Property(x => x.Permissions)
        .HasConversion(x => JsonSerializer.Serialize(x, jsonSerializerOptions),
                        x => JsonSerializer.Deserialize<HashSet<Permission>>(x, jsonSerializerOptions) ?? new()).Metadata.SetValueComparer(valueComparer);
    }
}

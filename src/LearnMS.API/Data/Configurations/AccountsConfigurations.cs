using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class ProfilesConfigurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => new { x.Id, x.ProviderType });

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ProviderType).HasConversion
        (
            x => x.ToString(),
            x => (ProviderType)Enum.Parse(typeof(ProviderType), x)
        );


        builder.HasIndex(x => new { x.ProviderId, x.ProviderType });

        builder.HasOne(x => x.User).WithMany(x => x.Accounts).HasForeignKey(x => x.Id);
    }
}

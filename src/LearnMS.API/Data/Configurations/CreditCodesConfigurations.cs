using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class CreditCodesConfigurations : IEntityTypeConfiguration<CreditCode>
{
    public void Configure(EntityTypeBuilder<CreditCode> builder)
    {
        builder.HasKey(x => x.Code);

        builder.Property(x => x.Code).HasMaxLength(20);

        builder.Property(x => x.Code).HasMaxLength(20);

        builder.Property(x => x.Status).HasConversion(x => x.ToString(), x => (CreditCodeStatus)Enum.Parse(typeof(CreditCodeStatus), x));

        builder.HasOne<Student>().WithMany().HasForeignKey(x => x.StudentId);
        builder.HasOne<Assistant>().WithMany().HasForeignKey(x => x.AssistantId);
    }
}
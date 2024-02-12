using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class StudentCreditsConfigurations : IEntityTypeConfiguration<StudentCredit>
{
    public void Configure(EntityTypeBuilder<StudentCredit> builder)
    {

        builder.HasOne<Student>().WithOne().HasForeignKey<StudentCredit>(x => x.StudentId);
        builder.HasOne<Assistant>().WithOne().HasForeignKey<StudentCredit>(x => x.AssistantId);

        builder.HasIndex(x => x.AssistantId);
    }
}
using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class StudentsConfigurations : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasBaseType<User>();

        builder.Property(x => x.Level).HasConversion(x => x.ToString(), x => (StudentLevel)Enum.Parse(typeof(StudentLevel), x));

    }
}
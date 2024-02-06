using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class CourseItemConfigurations : IEntityTypeConfiguration<CourseItem>
{
    public void Configure(EntityTypeBuilder<CourseItem> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}
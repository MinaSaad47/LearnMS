using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class CoursesConfigurations : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // builder.HasMany<CourseItem>("_items").WithOne().HasForeignKey(x => x.CourseId);

        // builder.Ignore(x => x.Items);

        //builder.Property(x => x.Items).HasField("_items").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.CourseId);


        builder.Property(x => x.Status).HasConversion(
            v => v.ToString(),
            v => (CourseStatus)Enum.Parse(typeof(CourseStatus), v)
        );
    }
}

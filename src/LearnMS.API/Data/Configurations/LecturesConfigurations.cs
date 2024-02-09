using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class LecturesConfigurations : IEntityTypeConfiguration<Lecture>
{
    public void Configure(EntityTypeBuilder<Lecture> builder)
    {
        // builder.HasMany<LectureItem>("_items").WithOne().HasForeignKey(x => x.LectureId).OnDelete(DeleteBehavior.Cascade);

        // builder.Ignore(x => x.Items);

        // builder.Property(x => x.Items).HasField("_items").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.LectureId);

        builder.HasOne<CourseItem>().WithOne().HasForeignKey<Lecture>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
    }
}
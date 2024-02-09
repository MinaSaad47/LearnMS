using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public class LessonsConfigurations : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasOne<LectureItem>().WithOne().HasForeignKey<Lesson>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
    }
}
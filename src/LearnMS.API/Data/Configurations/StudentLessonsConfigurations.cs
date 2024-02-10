using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class StudentLessonConfigurations : IEntityTypeConfiguration<StudentLesson>
{
    public void Configure(EntityTypeBuilder<StudentLesson> builder)
    {

        builder.HasKey(x => new { x.StudentId, x.LessonId });

        builder.HasOne<Lesson>().WithOne().HasForeignKey<StudentLesson>(x => x.LessonId);
        builder.HasOne<Student>().WithOne().HasForeignKey<StudentLesson>(x => x.StudentId);
    }
}
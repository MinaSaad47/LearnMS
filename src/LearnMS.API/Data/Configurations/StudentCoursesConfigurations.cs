using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class StudentCourseConfigurations : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {

        builder.HasKey(x => new { x.StudentId, x.CourseId });

        builder.HasOne<Course>().WithOne().HasForeignKey<StudentCourse>(x => x.CourseId);
        builder.HasOne<Student>().WithOne().HasForeignKey<StudentCourse>(x => x.StudentId);
    }
}
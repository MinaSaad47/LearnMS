using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class StudentLectureConfigurations : IEntityTypeConfiguration<StudentLecture>
{
    public void Configure(EntityTypeBuilder<StudentLecture> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.LectureId });

        builder.HasOne<Lecture>().WithOne().HasForeignKey<StudentLecture>(x => x.LectureId);
        builder.HasOne<Student>().WithOne().HasForeignKey<StudentLecture>(x => x.StudentId);
    }
}
using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class StudentExamsConfigurations : IEntityTypeConfiguration<StudentExam>
{
    public void Configure(EntityTypeBuilder<StudentExam> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.ExamId });

        builder.HasOne<Exam>().WithOne().HasForeignKey<StudentExam>(x => x.ExamId);
        builder.HasOne<Student>().WithOne().HasForeignKey<StudentExam>(x => x.StudentId);
    }
}
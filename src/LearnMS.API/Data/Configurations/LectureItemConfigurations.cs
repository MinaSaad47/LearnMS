using LearnMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMS.API.Data.Configurations;

public sealed class LectureItemConfigurations : IEntityTypeConfiguration<LectureItem>
{
    public void Configure(EntityTypeBuilder<LectureItem> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}
using LearnMS.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SmartEnum.EFCore;

namespace LearnMS.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigureSmartEnum();
        configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        base.ConfigureConventions(configurationBuilder);
    }

    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<Assistant> Assistants { get; set; } = default!;
    public DbSet<Teacher> Teachers { get; set; } = default!;
    public DbSet<Student> Students { get; set; } = default!;
    public DbSet<Lesson> Lessons { get; set; } = default!;
    public DbSet<Lecture> Lectures { get; set; } = default!;
    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<CreditCode> CreditCodes { get; set; } = default!;
}
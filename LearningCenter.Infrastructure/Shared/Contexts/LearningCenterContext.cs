using Domain;
using LearningCenter.Domain.Security.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Contexts;

public class LearningCenterContext : DbContext
{
    public LearningCenterContext()
    {
    }

    public LearningCenterContext(DbContextOptions<LearningCenterContext> options) : base(options)
    {
    }

    public DbSet<Tutorial> Tutorials { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=localhost,3306;Uid=root;Pwd=Upc123!;Database=learningcenterwx53;",
                serverVersion);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("User");
        builder.Entity<Tutorial>().ToTable("Tutorial");
        //builder.Entity<Tutorial>().HasKey(p => p.Id);
        //builder.Entity<Tutorial>().Property(p => p.Name).IsRequired().HasMaxLength(25);

        builder.Entity<Section>().ToTable("Section");
    }
}
using Domain;
using LearningCenter.Domain.Security.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LearningCenter.Infraestructure.Shared.Contexts;

public class LearningCenterContext : DbContext
{
    private readonly IConfiguration _configuration;
    public LearningCenterContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public LearningCenterContext(DbContextOptions<LearningCenterContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Tutorial?> Tutorials { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql(_configuration["ConnectionStrings:learningCenterConnection"],
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
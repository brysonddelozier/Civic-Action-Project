using Microsoft.EntityFrameworkCore;
using CivicAction.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CivicAction.Data;

public class CivicActionContext : IdentityDbContext<AppUser>
{
    
    public CivicActionContext(DbContextOptions<CivicActionContext> options)
        : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Update> Updates { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Inclusion> Inclusions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>().ToTable("Account");
        modelBuilder.Entity<Project>().ToTable("Project");
        modelBuilder.Entity<Update>().ToTable("Update");
        modelBuilder.Entity<Inclusion>().ToTable("Inclusion");
    }
}
using Microsoft.EntityFrameworkCore;
using TestApp.Entities;

namespace TestApp.Database;

public class TestAppContext: DbContext
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<DeveloperStudio> DeveloperStudios => Set<DeveloperStudio>();
    
    public TestAppContext(DbContextOptions<TestAppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .HasOne(game => game.DeveloperStudio)
            .WithMany(studio => studio.Games)
            .OnDelete(DeleteBehavior.SetNull);
        
        base.OnModelCreating(modelBuilder);
    }
}
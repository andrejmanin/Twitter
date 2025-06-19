using Database.Models;

namespace Database;

using Microsoft.EntityFrameworkCore;

public class TwitterDbContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Follower> Followers => Set<Follower>();
    
    public TwitterDbContext(DbContextOptions<TwitterDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().ToTable("Posts");
        modelBuilder.Entity<Comment>().ToTable("Comments");
        modelBuilder.Entity<Country>().ToTable("Countries");
        modelBuilder.Entity<City>().ToTable("Cities");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Follower>().ToTable("Followers");
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Gmail)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Nickname)
            .IsUnique();
        
        modelBuilder.Entity<Follower>()
            .HasKey(f => new { f.UserId, f.FollowerId });

        modelBuilder.Entity<Follower>()
            .HasOne(f => f.User)
            .WithMany(u => u.Followers)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Follower>()
            .HasOne(f => f.FollowerUser)
            .WithMany(u => u.Following)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
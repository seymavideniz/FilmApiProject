using FilmProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmProject.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Film> Films { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> User { get; set; }

    public DbSet<FilmDetails> FilmDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "User ID=myuser;Password=mypassword;Host=localhost;Port=5432;Database=mydatabase;Pooling=true;Maximum Pool Size=5;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>()
            .HasOne(f => f.Category)
            .WithMany()
            .HasForeignKey(f => f.CategoryId);

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = "Action" },
            new Category { CategoryId = 2, CategoryName = "Comedy" },
            new Category { CategoryId = 3, CategoryName = "Drama" },
            new Category { CategoryId = 4, CategoryName = "Horror" },
            new Category { CategoryId = 5, CategoryName = "Romance" }
        );

          modelBuilder.Entity<FilmDetails>()
            .HasOne(fd => fd.User)
            .WithOne(u => u.Ratings)
            .HasForeignKey<FilmDetails>(fd => fd.UserId);

        modelBuilder.Entity<FilmDetails>()
            .HasOne(fd => fd.Film)
            .WithMany(f => f.FilmDetails)
            .HasForeignKey(fd => fd.MovieId);
        
        base.OnModelCreating(modelBuilder);
    }
}
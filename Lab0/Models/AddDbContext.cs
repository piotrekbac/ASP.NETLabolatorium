using Microsoft.EntityFrameworkCore;
using Lab0.Models;

namespace Lab0.Models;

public class AddDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }      // najprostszy sposób na dostanie się do kolekcji zawartej w bazie danych

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=d:\data\Books-gr1.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasData(
            new Book()
            {
                Id = 1, 
                Title = "Pan Tadeusz", 
                Author = "Adam Mickiewicz", 
                YearOfPublishing = 1834,
                ISBN = "1111111111"
            },
            new Book()
            {
                Id = 2, 
                Title = "Lalka", 
                Author = "Bolesław Prus",
                YearOfPublishing = 1890,
                ISBN = "2222222222"
            },
            new Book()
            {
                Id = 3,
                Title = "Professional C# 9",
                Author = "K.K. Martin",
                YearOfPublishing = 1990,
                ISBN = "3333333333"
            });

        modelBuilder.Entity<Organization>()
            .HasData(
                new Organization()
                {
                    Id = 1,
                    Name = "WSEI",
                    Description = "Informatyka BamBam"
                },
                new Organization()
                {
                    Id = 2,
                    Name = "UEK",
                    Description = "Ekonomia PamPam"
                },
                new Organization()
                {
                    Id = 3,
                    Name = "PK",
                    Description = "Architektura RaRa"
                }
            );
    }

public DbSet<Lab0.Models.Organization> Organization { get; set; } = default!;
}
